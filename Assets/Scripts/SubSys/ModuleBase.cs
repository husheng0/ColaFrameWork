﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventType = ColaFrame.EventType;


/// <summary>
/// 系统的类型
/// </summary>
public enum ModuleType : byte
{
    /// <summary>
    /// 登录系统
    /// </summary>
    Login = 1,
}

/// <summary>
/// 系统的Module层抽象基类
/// </summary>
public abstract class ModuleBase : IEventHandler
{
    /// <summary>
    /// 当前系统的类型
    /// </summary>
    public readonly ModuleType ModuleType;

    /// <summary>
    /// 消息-回调函数字典，接收到消息后调用字典中的回调方法
    /// </summary>
    protected Dictionary<string, MsgHandler> msgHanderDic;

    /// <summary>
    /// 系统是否进行过初始化
    /// </summary>
    public bool IsInit { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="moduleType"></param>系统类型
    public ModuleBase(ModuleType moduleType)
    {
        this.ModuleType = moduleType;
        msgHanderDic = null;
    }

    /// <summary>
    /// 初始化模块
    /// </summary>
    public virtual void Init()
    {
        IsInit = true;
        RegisterHander();
    }

    /// <summary>
    /// 退出系统
    /// </summary>
    public virtual void Exit()
    {
        UnRegisterHander();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    public virtual void Reset()
    {
        OnReset();
    }

    protected virtual void OnReset()
    {
        
    }

    /// <summary>
    /// 注册消息/事件回调
    /// </summary>
    protected virtual void RegisterHander()
    {
        msgHanderDic = null;
        GameEventMgr.GetInstance().RegisterHandler(this,EventType.ServerMsg);
    }

    /// <summary>
    /// 反注册消息/事件回调
    /// </summary>
    protected virtual void UnRegisterHander()
    {
        GameEventMgr.GetInstance().UnRegisterHandler(this);

        if (null != msgHanderDic)
        {
            msgHanderDic.Clear();
            msgHanderDic = null;
        }
    }

    /// <summary>
    /// 处理消息的函数的实现
    /// </summary>
    /// <param name="gameEvent"></param>事件
    /// <returns></returns>是否处理成功
    protected virtual bool HandleMessageImpl(GameEvent gameEvent)
    {
        bool handled = false;
        if (EventType.ServerMsg == gameEvent.EventType)
        {
            if (null != msgHanderDic)
            {
                EventData eventData=gameEvent.Para as EventData;
                if (null != eventData && msgHanderDic.ContainsKey(eventData.Cmd))
                {
                    msgHanderDic[eventData.Cmd](eventData);
                    handled = true;
                }
            }
        }
        return handled;
    }

    /// <summary>
    /// 是否处理了该消息的函数的实现
    /// </summary>
    /// <returns></returns>是否处理
    protected virtual bool IsHandlerImpl(GameEvent gameEvent)
    {
        bool handled = false;
        if (EventType.ServerMsg == gameEvent.EventType)
        {
            if (null != msgHanderDic)
            {
                EventData eventData = gameEvent.Para as EventData;
                if (null != eventData && msgHanderDic.ContainsKey(eventData.Cmd))
                {
                    handled = true;
                }
            }
        }
        return handled;
    }


    public bool HandleMessage(GameEvent evt)
    {
        return HandleMessageImpl(evt);
    }

    public bool IsHasHandler(GameEvent evt)
    {
        return IsHandlerImpl(evt);
    }
}
