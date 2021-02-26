/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-11:07:11 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneInfoController : BaseMVCController<SceneInfoModel, ISceneInfoView>
{

    public SceneInfoController(BaseMonoBehaviour content, ISceneInfoView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public SceneInfoBean GetSceneInfoData(Action<SceneInfoBean> action)
    {
        SceneInfoBean data = GetModel().GetSceneInfoData();
        if (data == null)
        {
            GetView().GetSceneInfoFail("没有数据", null);
            return null;
        }
        GetView().GetSceneInfoSuccess<SceneInfoBean>(data, action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllSceneInfoData(Action<List<SceneInfoBean>> action)
    {
        List<SceneInfoBean> listData = GetModel().GetAllSceneInfoData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetSceneInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetSceneInfoSuccess<List<SceneInfoBean>>(listData, action);
        }
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="action"></param>
    public void GetSceneInfoDataById(long id, Action<SceneInfoBean> action)
    {
        List<SceneInfoBean> listData = GetModel().GetSceneInfoDataById(id);
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetSceneInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetSceneInfoSuccess(listData[0], action);
        }
    }
}