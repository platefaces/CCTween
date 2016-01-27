//*********************************************************************
//
//							ScriptName:	CCTweener
//
//							Project	  : CCAnim
//
//*********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CCTweener : MonoBehaviour {
    public enum Style {
        Once,
        Loop,
        Repeatedly,
        PingPong
    }


    // 是否是在启动事运行;
    public bool IsStartRun = true;

    public Style style = Style.Once;

    public float Delay;

    public List<GameObject> NotifyObject = new List<GameObject>();
    public List<string> NotifyFun = new List<string>();

    void Reset() {  StartValue(); }

    void Awake() {  CCAwake(); }

    void Start()
    {
        CCStart();

        if (IsStartRun) 
        {
            Invoke("PlayForward", Delay);
        }


    }
    
    public virtual      void CCAwake()  { }
    public virtual      void CCStart()  { }
    public abstract     void PlayForward();
    public abstract     void PlayReverse();
    protected abstract  void StartValue ();




    public virtual void AddNotify() 
    {
        if (NotifyObject.Count >= 1 && NotifyObject[NotifyObject.Count - 1] == null)
            return;

        if (NotifyObject.Count == 0)
        {
            NotifyObject = new List<GameObject>();
            NotifyFun = new List<string>();
        }

        NotifyObject.Add(null);
        NotifyFun.Add(null);
    }

    public virtual void SubNotify()
    {
        if (NotifyObject.Count == 0)
            return;

        NotifyObject.RemoveAt(NotifyObject.Count - 1);
        NotifyFun.RemoveAt(NotifyFun.Count - 1);
    }

    public void NotifyComplete() 
    {
        for (int i = 0; i < NotifyObject.Count; i++)
        {
            if (!string.IsNullOrEmpty(NotifyFun[i]))
            {
#if UNITY_EDITOR
                try
                {
                    NotifyObject[i].SendMessage(NotifyFun[i]);
                }
                catch (UnityException e) 
                {
                    Debug.LogError("CCTween结束通知事件不成功,Object not found: " + NotifyObject[i].name + " function not found: " + NotifyFun[i]);
                }
#else
                NotifyObject[i].SendMessage(NotifyFun[i]);
#endif
            }
        }
    }


}
