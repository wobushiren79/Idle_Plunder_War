/*
* FileName: UserData 
* Author: AppleCoffee 
* CreateTime: 2021-03-01-11:02:15 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class UserDataService : BaseDataStorage<UserDataBean>
{
    protected readonly string saveFileName;

    public UserDataService()
    {
        saveFileName = "UserData";
    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<UserDataBean> QueryAllData()
    {
        return null; 
    }

    /// <summary>
    /// 查询游戏配置数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean QueryData()
    {
        return BaseLoadData(saveFileName);
    }
        
    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<UserDataBean> QueryDataById(long id)
    {
        return null;
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    public void UpdateData(UserDataBean data)
    {
        BaseSaveData(saveFileName, data);
    }
}