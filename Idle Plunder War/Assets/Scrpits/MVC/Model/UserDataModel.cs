/*
* FileName: UserData 
* Author: AppleCoffee 
* CreateTime: 2021-03-01-11:02:15 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UserDataModel : BaseMVCModel
{
    protected UserDataService serviceUserData;

    public override void InitData()
    {
        serviceUserData = new UserDataService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<UserDataBean> GetAllUserDataData()
    {
        List<UserDataBean> listData = serviceUserData.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean GetUserDataData()
    {
        UserDataBean data = serviceUserData.QueryData();
        if (data == null)
            data = new UserDataBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<UserDataBean> GetUserDataDataById(long id)
    {
        List<UserDataBean> listData = serviceUserData.QueryDataById(id);
        return listData;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetUserDataData(UserDataBean data)
    {
        serviceUserData.UpdateData(data);
    }

}