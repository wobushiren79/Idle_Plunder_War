/*
* FileName: TreasureInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-09:59:26 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreasureInfoController : BaseMVCController<TreasureInfoModel, ITreasureInfoView>
{

    public TreasureInfoController(BaseMonoBehaviour content, ITreasureInfoView view) : base(content, view)
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
    public TreasureInfoBean GetTreasureInfoData(Action<TreasureInfoBean> action)
    {
        TreasureInfoBean data = GetModel().GetTreasureInfoData();
        if (data == null) {
            GetView().GetTreasureInfoFail("没有数据",null);
            return null;
        }
        GetView().GetTreasureInfoSuccess<TreasureInfoBean>(data,action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllTreasureInfoData(Action<List<TreasureInfoBean>> action)
    {
        List<TreasureInfoBean> listData = GetModel().GetAllTreasureInfoData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetTreasureInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetTreasureInfoSuccess<List<TreasureInfoBean>>(listData, action);
        }
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="action"></param>
    public void GetTreasureInfoDataById(long id,Action<TreasureInfoBean> action)
    {
        List<TreasureInfoBean> listData = GetModel().GetTreasureInfoDataById(id);
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetTreasureInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetTreasureInfoSuccess(listData[0], action);
        }
    }
} 