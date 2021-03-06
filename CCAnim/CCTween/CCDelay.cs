﻿//*********************************************************************
//
//							ScriptName:	CCDelay
//
//							Project	  : CCAnim
//
//*********************************************************************

using System;
using UnityEngine;
using System.Collections;

public class CCDelay : CCAction {

    public static CCDelay Create(Action action, float time)
    {
        return new CCDelay
        {
            MyAction  = action,
            _duration = time
        };
    }
    public delegate void OnAction(CCAction tr, params object[] obj);

    private  OnAction MyOnAction; 
    private  object[] Params { get; set; }


    public Action MyAction;
    public void SetHandle(OnAction onAction, params object[] _params)
    {
        MyOnAction  = onAction;
        Params      = _params;
    }
    protected override void EndRun()
    {
        if (MyAction != null)
        {
            MyAction();  
        }
        if (MyOnAction != null)
            MyOnAction(this, Params);
    }
  
}
