﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用工具类，为导出lua接口调用准备
/// </summary>
public static class Common_Utils
{
    /// <summary>
    /// 给按钮添加点击事件(以后可以往这里添加点击声音)
    /// </summary>
    /// <param name="go"></param>
    /// <param name="callback"></param>
    public static void AddBtnMsg(GameObject go, Action<GameObject> callback)
    {
        CommonHelper.AddBtnMsg(go, callback);
    }

    /// <summary>
    /// 删除一个按钮的点击事件
    /// </summary>
    /// <param name="go"></param>
    /// <param name="callback"></param>
    public static void RemoveBtnMsg(GameObject go, Action<GameObject> callback)
    {
        CommonHelper.RemoveBtnMsg(go, callback);
    }

    /// <summary>
    /// 根据路径实例化一个Prefab
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstantiateGoByPath(string path, GameObject parent)
    {
        return null;
    }

    /// <summary>
    /// 根据一个预制实例化一个Prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstantiateGoByPrefab(GameObject prefab, GameObject parent)
    {
        return CommonHelper.InstantiateGoByPrefab(prefab, parent);
    }

    /// <summary>
    /// 给物体添加一个单一组件
    /// </summary>
    /// <param name="go"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Component AddSingleComponent(this GameObject go,Type type)
    {
        if (null != go)
        {
            Component component = go.GetComponent(type);
            if (null == component)
            {
                component = go.AddComponent(type);
            }
            return component;
        }
        Debug.LogWarning("要添加组件的物体为空！");
        return null;
    }

    /// <summary>
    /// 获取某个物体下对应名字的子物体(如果有重名的，就返回第一个符合的)
    /// </summary>
    /// <param name="go"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public static GameObject GetGameObjectByName(this GameObject go, string childName)
    {
        GameObject ret = null;
        if (go != null)
        {
            Transform[] childrenObj = go.GetComponentsInChildren<Transform>(true);
            if (childrenObj != null)
            {
                for (int i = 0; i < childrenObj.Length; ++i)
                {
                    if ((childrenObj[i].name == childName))
                    {
                        ret = childrenObj[i].gameObject;
                        break;
                    }
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// 根据路径查找物体(可以是自己本身/子物体/父节点)
    /// </summary>
    /// <param name="obj"></param>父物体节点
    /// <param name="childPath"></param>子物体路径+子物体名称
    /// <returns></returns>
    public static GameObject FindChildByPath(this GameObject obj, string childPath)
    {
        if (null == obj)
        {
            Debug.LogWarning("FindChildByPath方法传入的根节点为空！");
            return null;
        }
        if ("." == childPath) return obj;
        if (".." == childPath)
        {
            Transform parentTransform = obj.transform.parent;
            return parentTransform == null ? null : parentTransform.gameObject;
        }
        Transform transform = obj.transform.Find(childPath);
        return null != transform ? transform.gameObject : null;
    }


    /// <summary>
    /// 根据路径查找物体上的某个类型的组件(可以是自己本身/子物体/父节点)
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="childPath"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Component GetComponentByPath(this GameObject obj, string childPath, Type type)
    {
        GameObject childObj = FindChildByPath(obj, childPath);
        if (null == childObj)
        {
            return null;
        }
        Component component = childObj.GetComponent(type);
        if (null == component)
        {
            Debug.LogWarning(String.Format("没有在路径:{0}上找到组件:{1}!", childPath, type));
            return null;
        }
        return component;
    }

    /// <summary>
    /// 获取当前运行的设备平台信息
    /// </summary>
    /// <returns></returns>
    public static string GetDeviceInfo()
    {
        return CommonHelper.GetDeviceInfo();
    }

    /// <summary>
    /// 返回UI画布的根节点
    /// </summary>
    /// <returns></returns>
    public static GameObject GetUIRootObj()
    {
        return GUIHelper.GetUIRootObj();
    }

    /// <summary>
    /// 返回UI相机节点
    /// </summary>
    /// <returns></returns>
    public static GameObject GetUICameraObj()
    {
        return GUIHelper.GetUICameraObj();
    }

    /// <summary>
    /// 返回UI画布
    /// </summary>
    /// <returns></returns>
    public static Canvas GetUIRoot()
    {
        return GUIHelper.GetUIRoot();
    }

    /// <summary>
    /// 返回UI相机
    /// </summary>
    /// <returns></returns>
    public static Camera GetUICamera()
    {
        return GUIHelper.GetUICamera();
    }

    /// <summary>
    /// 获取主相机
    /// </summary>
    /// <returns></returns>
    public static Camera GetMainCamera()
    {
        return GUIHelper.GetMainCamera();
    }

    /// <summary>
    /// 获取主相机节点
    /// </summary>
    /// <returns></returns>
    public static GameObject GetMainGameObj()
    {
        return GUIHelper.GetMainGameObj();
    }

    /// <summary>
    /// 获取模型描边相机节点
    /// </summary>
    /// <returns></returns>
    public static GameObject GetModelOutlineCameraObj()
    {
        return GUIHelper.GetModelOutlineCameraObj();
    }
}