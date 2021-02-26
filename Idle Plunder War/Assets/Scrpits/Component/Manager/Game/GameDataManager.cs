using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataManager : BaseManager,IGameConfigView,IBaseDataView
{    
    //游戏设置
    public GameConfigBean gameConfig;

    public GameConfigController controllerForGameConfig;
    public BaseDataController baseDataController;

    protected void Awake()
    {
        controllerForGameConfig = new GameConfigController(this, this);
        controllerForGameConfig.GetGameConfigData();
        baseDataController = new BaseDataController(this,this);
        baseDataController.InitAllBaseData();
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
    #endregion
}