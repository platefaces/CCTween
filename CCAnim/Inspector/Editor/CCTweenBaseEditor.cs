using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[CustomEditor(typeof(CCTweener))]
public abstract class CCTweenBaseEditor : Editor
{
    protected List<string> comAndfunNames = new List<string>();
    protected List<string> funNames = new List<string>();

    protected CCTweener _CCTweener;

    public void Awake() 
    {
        _CCTweener = (CCTweener)target;
    }

    public override void OnInspectorGUI() 
    {
        StartInspectorGUI();

        MiddleInspectorGUI();

        EndInspectorGUI();
    }

    public abstract void StartInspectorGUI();
    public virtual void EndInspectorGUI() 
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            _CCTweener.AddNotify();
        }
        if (GUILayout.Button("-"))
        {
            _CCTweener.SubNotify();
        }
        GUILayout.EndHorizontal();
    }

    public virtual void MiddleInspectorGUI() 
    {
        GUILayout.Label("NotifyComplete");

        //通知事件
        if (_CCTweener.NotifyObject != null && _CCTweener.NotifyObject.Count > 0)
        {
            //循环有多少个通知对象
            for (int i = 0; i < _CCTweener.NotifyObject.Count; i++)
            {
                _CCTweener.NotifyObject[i] = (GameObject)EditorGUILayout.ObjectField(_CCTweener.NotifyObject[i], typeof(GameObject));

                if (_CCTweener.NotifyObject[i] != null)
                {
                    MonoBehaviour[] coms = _CCTweener.NotifyObject[i].GetComponents<MonoBehaviour>();

                    if (coms == null)
                        break;

                    int tempIndex = -1;

                    //循环每一个组件
                    comAndfunNames.Clear();
                    for (int k = 0; k < coms.Length; k++)
                    {
                        if (coms[k] is UnityEngine.UI.Image)
                            continue;

                        Type type = coms[k].GetType();
                        System.Reflection.MemberInfo[] memberInfos = type.FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance,
                             new MemberFilter(DelegateToSearchCriteria), ".*");

                        for (int j = 0; j < memberInfos.Length; j++)
                        {
                            comAndfunNames.Add(type.Name + "/" + memberInfos[j].Name);
                            funNames.Add(memberInfos[j].Name);

                            if (_CCTweener.NotifyFun != null && _CCTweener.NotifyFun[i] == memberInfos[j].Name)
                            {
                                tempIndex = j;
                            }
                        }
                    }

                    if (comAndfunNames.Count == 0)
                        continue;

                    if (tempIndex == -1)
                    {
                        tempIndex = EditorGUILayout.Popup(tempIndex, new string[] { "No Found Function" });
                        tempIndex = 0;
                    }
                    else
                    {
                        tempIndex = EditorGUILayout.Popup(tempIndex, comAndfunNames.ToArray());
                    }

                    _CCTweener.NotifyFun[i] = funNames[tempIndex];
                    funNames.Clear();
                }
            }
        }
    }

    //只搜索以On开头的函数
    public static bool DelegateToSearchCriteria(MemberInfo objMemberInfo, System.Object objSearch)
    {
        //string regex = @"^[On][Play][OnClick]";
        //bool isRegsx = Regex.IsMatch(objMemberInfo.Name, regex);

        //if (isRegsx) 
        //{
        //    Debug.Log("匹配成功: " + objMemberInfo.Name);
        //}


        return objMemberInfo.Name.StartsWith("On");
    }
}
