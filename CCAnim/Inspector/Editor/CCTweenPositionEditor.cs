//*********************************************************************
//
//							ScriptName:	CCTweenPositionEditor
//
//							Project	  : CCAnim
//
//*********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;


[CustomEditor(typeof(CCTweenPosition))]
public class CCTweenPositionEditor : CCTweenBaseEditor
{
    protected CCTweenPosition _CCTarget;

    void OnEnable()
    {
        _CCTarget = (CCTweenPosition)_CCTweener;
    }


    public override void StartInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        _CCTarget.FormPosition = EditorGUILayout.Vector3Field("Start Position：", _CCTarget.FormPosition);
        if (GUILayout.Button("复制坐标", new GUILayoutOption[] { GUILayout.Width(80) }))
        {
            _CCTarget.FormPosition = _CCTarget.MyPosition;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        _CCTarget.ToPosition = EditorGUILayout.Vector3Field("End Position：", _CCTarget.ToPosition);
        if (GUILayout.Button("复制坐标", new GUILayoutOption[] { GUILayout.Width(80) }))
        {
            _CCTarget.ToPosition = _CCTarget.MyPosition;
        }
        EditorGUILayout.EndHorizontal();

        _CCTarget.style = (CCTweener.Style)EditorGUILayout.EnumPopup("Anim Type：", _CCTarget.style);
        _CCTarget.MoveTime = EditorGUILayout.FloatField("Anim Play Time :", _CCTarget.MoveTime);
        _CCTarget.IsStartRun = EditorGUILayout.Toggle("Is Start Play：", _CCTarget.IsStartRun);
        _CCTarget.Delay = EditorGUILayout.FloatField("delay：", _CCTarget.Delay);
    }

}
