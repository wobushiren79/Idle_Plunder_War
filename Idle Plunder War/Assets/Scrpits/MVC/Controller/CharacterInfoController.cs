/*
* FileName: CharacterInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-10:29:33 
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterInfoController : BaseMVCController<CharacterInfoModel, ICharacterInfoView>
{

    public CharacterInfoController(BaseMonoBehaviour content, ICharacterInfoView view) : base(content, view)
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
    public CharacterInfoBean GetCharacterInfoData(Action<CharacterInfoBean> action)
    {
        CharacterInfoBean data = GetModel().GetCharacterInfoData();
        if (data == null) {
            GetView().GetCharacterInfoFail("没有数据",null);
            return null;
        }
        GetView().GetCharacterInfoSuccess(data,action);
        return data;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="action"></param>
    public void GetAllCharacterInfoData(Action<List<CharacterInfoBean>> action)
    {
        List<CharacterInfoBean> listData = GetModel().GetAllCharacterInfoData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetCharacterInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetCharacterInfoSuccess(listData, action);
        }
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="action"></param>
    public void GetCharacterInfoDataById(long id,Action<CharacterInfoBean> action)
    {
        List<CharacterInfoBean> listData = GetModel().GetCharacterInfoDataById(id);
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetCharacterInfoFail("没有数据", null);
        }
        else
        {
            GetView().GetCharacterInfoSuccess(listData[0], action);
        }
    }
} 