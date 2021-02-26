using System;
using UnityEditor;
using UnityEngine;

public class GameManager : BaseManager, ISceneInfoView
{
    public GameBean gameData;
    public SceneInfoBean sceneInfoData;

    public SceneInfoController sceneInfoController;

    public void Awake()
    {
        sceneInfoController = new SceneInfoController(this, this);
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

    #region 数据回调
    public void GetSceneInfoSuccess<T>(T data, Action<T> action)
    {
        this.sceneInfoData = data as SceneInfoBean;
        action?.Invoke(data);
    }

    public void GetSceneInfoFail(string failMsg, Action action)
    {

    }
    #endregion 
}