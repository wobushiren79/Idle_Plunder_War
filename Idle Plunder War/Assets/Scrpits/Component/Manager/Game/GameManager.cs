using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : BaseManager, ISceneInfoView, ILevelInfoView
{
    public GameBean gameData;
    public SceneInfoBean sceneInfoData;

    public Dictionary<int, LevelInfoBean> dicPowerData = new Dictionary<int, LevelInfoBean>();
    public Dictionary<int, LevelInfoBean> dicPriceData = new Dictionary<int, LevelInfoBean>();
    public Dictionary<int, LevelInfoBean> dicNumberData = new Dictionary<int, LevelInfoBean>();
    public Dictionary<int, LevelInfoBean> dicLevelUpData = new Dictionary<int, LevelInfoBean>();

    public SceneInfoController sceneInfoController;
    public LevelInfoController controllerForLevelInfo;

    public void Awake()
    {
        sceneInfoController = new SceneInfoController(this, this);

        controllerForLevelInfo = new LevelInfoController(this, this);
        controllerForLevelInfo.GetAllLevelInfoDataForPower((listData) => { SetLevelInfoData(dicPowerData, listData); });
        controllerForLevelInfo.GetAllLevelInfoDataForPrice((listData) => { SetLevelInfoData(dicPriceData, listData); });
        controllerForLevelInfo.GetAllLevelInfoDataForNumber((listData) => { SetLevelInfoData(dicNumberData, listData); });
        controllerForLevelInfo.GetAllLevelInfoDataForLevelUp((listData) => { SetLevelInfoData(dicLevelUpData, listData); });
    }

    public GameBean InitGameData()
    {
        gameData = new GameBean();
        return gameData;
    }

    public void GetSceneInfoById(long id, Action<SceneInfoBean> action)
    {
        sceneInfoController.GetSceneInfoDataById(id, action);
    }

    public SceneInfoBean GetSceneInfo()
    {
        return sceneInfoData;
    }

    /// <summary>
    /// 获取升级信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public LevelInfoBean GetLevelInfoForPower(int level)
    {
        return GetLevelInfo(dicPowerData, level);
    }

    /// <summary>
    /// 获取升级信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public LevelInfoBean GetLevelInfoForPrice(int level)
    {
        return GetLevelInfo(dicPriceData, level);
    }


    /// <summary>
    /// 获取升级信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public LevelInfoBean GetLevelInfoForNumber(int level)
    {
        return GetLevelInfo(dicNumberData, level);
    }

    /// <summary>
    /// 获取升级信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public LevelInfoBean GetLevelInfoForLevelUp(int level)
    {
        return GetLevelInfo(dicLevelUpData, level);
    }

    protected LevelInfoBean GetLevelInfo(Dictionary<int, LevelInfoBean> dic, int level)
    {
        if (dic == null)
            return null;
        if (dic.TryGetValue(level, out LevelInfoBean value))
        {
            return value;
        }
        return null;
    }
    protected void SetLevelInfoData(Dictionary<int, LevelInfoBean> dic, List<LevelInfoBean> listData)
    {
        dic.Clear();
        for (int i = 0; i < listData.Count; i++)
        {
            LevelInfoBean levelInfo = listData[i];
            dic.Add(levelInfo.level, levelInfo);
        }
    }

    #region 数据回调
    public void GetSceneInfoSuccess<T>(T data, Action<T> action)
    {
        this.sceneInfoData = data as SceneInfoBean;
        action?.Invoke(data);
    }

    public void GetSceneInfoFail(string failMsg, Action action)
    {

    }
    public void GetLevelInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetLevelInfoFail(string failMsg, Action action)
    {
    }
    #endregion 
}