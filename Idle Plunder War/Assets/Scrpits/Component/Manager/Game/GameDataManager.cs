using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataManager : BaseManager, IGameConfigView, IBaseDataView, IUserDataView
{
    //游戏设置
    public GameConfigBean gameConfig;
    public UserDataBean userData;

    public GameConfigController controllerForGameConfig;
    public UserDataController controllerForUserData;
    public BaseDataController controllerForBaseData;

    protected void Awake()
    {
        controllerForGameConfig = new GameConfigController(this, this);
        controllerForGameConfig.GetGameConfigData();
        controllerForBaseData = new BaseDataController(this, this);
        controllerForBaseData.InitAllBaseData();
        controllerForUserData = new UserDataController(this, this);
        controllerForUserData.GetUserDataData(null);
    }

    /// <summary>
    /// 保存游戏设置
    /// </summary>
    public void SaveGameConfig()
    {
        controllerForGameConfig.SaveGameConfigData(gameConfig);
    }

    /// <summary>
    /// 获取游戏设置
    /// </summary>
    /// <returns></returns>
    public GameConfigBean GetGameConfig()
    {
        return gameConfig;
    }

    /// <summary>
    /// 获取用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean GetUserData()
    {
        return userData;
    }

    #region 游戏数据回掉
    public void GetGameConfigFail()
    {

    }

    public void GetGameConfigSuccess(GameConfigBean configBean)
    {
        gameConfig = configBean;
    }

    public void SetGameConfigFail()
    {

    }

    public void SetGameConfigSuccess(GameConfigBean configBean)
    {

    }

    public void GetAllBaseDataSuccess(List<BaseDataBean> listData)
    {

    }

    public void GetAllBaseDataFail(string failMsg)
    {

    }

    public void GetUserDataSuccess<T>(T data, Action<T> action)
    {
        userData = data as UserDataBean;
        action?.Invoke(data);
    }

    public void GetUserDataFail(string failMsg, Action action)
    {

    }
    #endregion
}