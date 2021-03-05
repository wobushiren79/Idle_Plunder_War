/*
* FileName: BuildingInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-15:14:57 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BuildingInfoService : BaseMVCService
{
    public BuildingInfoService() : base("building_info", "")
    {

    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<BuildingInfoBean> QueryAllData()
    {
        List<BuildingInfoBean> listData = BaseQueryAllData<BuildingInfoBean>();
        return listData; 
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <returns></returns>
    public BuildingInfoBean QueryData()
    {
        return null; 
    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<BuildingInfoBean> QueryDataById(long id)
    {
        return BaseQueryData<BuildingInfoBean>("link_id", "id", id + "");
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool UpdateData(BuildingInfoBean data)
    {
        bool deleteState = BaseDeleteDataById(data.id);
        if (deleteState)
        {
            bool insertSuccess = BaseInsertData(tableNameForMain, data);
            return insertSuccess;
        }
        return false;
    }
}