/*
* FileName: LevelInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-03-10:12:03 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelInfoModel : BaseMVCModel
{
    protected LevelInfoService serviceLevelInfo;

    public override void InitData()
    {
        serviceLevelInfo = new LevelInfoService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<LevelInfoBean> GetAllLevelInfoDataForPower()
    {
        List<LevelInfoBean> listData = serviceLevelInfo.QueryAllDataForPower();
        return listData;
    }
    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<LevelInfoBean> GetAllLevelInfoDataForPrice()
    {
        List<LevelInfoBean> listData = serviceLevelInfo.QueryAllDataForPrice();
        return listData;
    }
    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<LevelInfoBean> GetAllLevelInfoDataForNumber()
    {
        List<LevelInfoBean> listData = serviceLevelInfo.QueryAllDataForNumber();
        return listData;
    }
    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<LevelInfoBean> GetAllLevelInfoDataForLevelUp()
    {
        List<LevelInfoBean> listData = serviceLevelInfo.QueryAllDataForLevelUp();
        return listData;
    }
}