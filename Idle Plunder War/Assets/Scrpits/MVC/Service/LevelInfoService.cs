/*
* FileName: LevelInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-03-10:12:03 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelInfoService : BaseMVCService
{
    public string tableNameForPower;
    public string tableNameForPrice;
    public string tableNameForNumber;
    public string tableNameForLevelUp;

    public LevelInfoService() : base("", "")
    {
        tableNameForPower = "level_power";
        tableNameForPrice = "level_price";
        tableNameForNumber = "level_number";
        tableNameForLevelUp = "level_up";
    }

    public List<LevelInfoBean> QueryAllDataForPower()
    {
        tableNameForMain = tableNameForPower;
        return BaseQueryAllData<LevelInfoBean>();
    }

    public List<LevelInfoBean> QueryAllDataForPrice()
    {
        tableNameForMain = tableNameForPrice;
        return BaseQueryAllData<LevelInfoBean>();
    }

    public List<LevelInfoBean> QueryAllDataForNumber()
    {
        tableNameForMain = tableNameForNumber;
        return BaseQueryAllData<LevelInfoBean>();
    }

    public List<LevelInfoBean> QueryAllDataForLevelUp()
    {
        tableNameForMain = tableNameForLevelUp;
        return BaseQueryAllData<LevelInfoBean>();
    }
}