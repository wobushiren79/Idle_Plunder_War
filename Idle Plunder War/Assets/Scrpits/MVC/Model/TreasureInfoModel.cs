/*
* FileName: TreasureInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-09:59:26 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TreasureInfoModel : BaseMVCModel
{
    protected TreasureInfoService serviceTreasureInfo;

    public override void InitData()
    {
        serviceTreasureInfo = new TreasureInfoService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<TreasureInfoBean> GetAllTreasureInfoData()
    {
        List<TreasureInfoBean> listData = serviceTreasureInfo.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public TreasureInfoBean GetTreasureInfoData()
    {
        TreasureInfoBean data = serviceTreasureInfo.QueryData();
        if (data == null)
            data = new TreasureInfoBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<TreasureInfoBean> GetTreasureInfoDataById(long id)
    {
        List<TreasureInfoBean> listData = serviceTreasureInfo.QueryDataById(id);
        return listData;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetTreasureInfoData(TreasureInfoBean data)
    {
        serviceTreasureInfo.UpdateData(data);
    }

}