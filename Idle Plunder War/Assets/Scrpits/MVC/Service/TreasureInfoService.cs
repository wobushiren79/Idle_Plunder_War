/*
* FileName: TreasureInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-09:59:26 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TreasureInfoService : BaseMVCService
{
    public TreasureInfoService() : base("treasure_info", "")
    {

    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<TreasureInfoBean> QueryAllData()
    {
        List<TreasureInfoBean> listData = BaseQueryAllData<TreasureInfoBean>();
        return listData; 
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <returns></returns>
    public TreasureInfoBean QueryData()
    {
        return null; 
    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<TreasureInfoBean> QueryDataById(long id)
    {
        return BaseQueryData<TreasureInfoBean>("link_id", "id", id + "");
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool UpdateData(TreasureInfoBean data)
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