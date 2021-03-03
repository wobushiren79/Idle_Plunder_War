/*
* FileName: LevelInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-03-10:12:03 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelInfoController : BaseMVCController<LevelInfoModel, ILevelInfoView>
{

    public LevelInfoController(BaseMonoBehaviour content, ILevelInfoView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllLevelInfoDataForPower(Action<List<LevelInfoBean>> action)
    {
        List<LevelInfoBean> listData = GetModel().GetAllLevelInfoDataForPower();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetLevelInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetLevelInfoSuccess(listData, action);
        }
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllLevelInfoDataForPrice(Action<List<LevelInfoBean>> action)
    {
        List<LevelInfoBean> listData = GetModel().GetAllLevelInfoDataForPrice();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetLevelInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetLevelInfoSuccess(listData, action);
        }
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllLevelInfoDataForNumber(Action<List<LevelInfoBean>> action)
    {
        List<LevelInfoBean> listData = GetModel().GetAllLevelInfoDataForNumber();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetLevelInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetLevelInfoSuccess(listData, action);
        }
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllLevelInfoDataForLevelUp(Action<List<LevelInfoBean>> action)
    {
        List<LevelInfoBean> listData = GetModel().GetAllLevelInfoDataForLevelUp();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetLevelInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetLevelInfoSuccess(listData, action);
        }
    }
} 