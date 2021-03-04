/*
* FileName: CharacterInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-10:29:33 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CharacterInfoModel : BaseMVCModel
{
    protected CharacterInfoService serviceCharacterInfo;

    public override void InitData()
    {
        serviceCharacterInfo = new CharacterInfoService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<CharacterInfoBean> GetAllCharacterInfoData()
    {
        List<CharacterInfoBean> listData = serviceCharacterInfo.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public CharacterInfoBean GetCharacterInfoData()
    {
        CharacterInfoBean data = serviceCharacterInfo.QueryData();
        if (data == null)
            data = new CharacterInfoBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<CharacterInfoBean> GetCharacterInfoDataById(long id)
    {
        List<CharacterInfoBean> listData = serviceCharacterInfo.QueryDataById(id);
        return listData;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetCharacterInfoData(CharacterInfoBean data)
    {
        serviceCharacterInfo.UpdateData(data);
    }

}