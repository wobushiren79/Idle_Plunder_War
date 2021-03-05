using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreasureManager : BaseManager, ITreasureInfoView
{
    protected TreasureInfoController controllerForTreasureInfo;
    //宝藏列表
    protected Treasure treasure; 
    //宝藏模型数据
    public Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();
    //宝藏信息数据
    public Dictionary<long, TreasureInfoBean> dicTreasureInfo = new Dictionary<long, TreasureInfoBean>();

    private void Awake()
    {
        InitAllTreasureInfo();
    }

    public void SetTreasure(Treasure treasure)
    {
        this.treasure = treasure;
    }

    public Treasure GetTreasure()
    {
        return treasure;
    }

    /// <summary>
    /// 初始化所有数据
    /// </summary>
    public void InitAllTreasureInfo()
    {
        if (controllerForTreasureInfo == null)
        {
            controllerForTreasureInfo = new TreasureInfoController(this, this);
        }
        Action<List<TreasureInfoBean>> callBack = (listData) =>
        {
            dicTreasureInfo.Clear();
            for (int i = 0; i < listData.Count; i++)
            {
                TreasureInfoBean itemData = listData[i];
                dicTreasureInfo.Add(itemData.id, itemData);
            }
        };
        controllerForTreasureInfo.GetAllTreasureInfoData(callBack);
    }


    /// <summary>
    /// 获取宝藏信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TreasureInfoBean GetTreasureInfo(long id)
    {
        if (dicTreasureInfo == null)
            return null;
        if (dicTreasureInfo.TryGetValue(id, out TreasureInfoBean treasureInfo))
        {
            return treasureInfo;
        }
        return null;
    }

    /// <summary>
    /// 获取基础模型
    /// </summary>
    /// <param name="characterCamp"></param>
    /// <returns></returns>
    public GameObject GetTreasureBaseModel()
    {
        GameObject objBaseModel = GetModel(dicModel, "treasure/base", "TreasureBase", "Assets/Prefabs/Treasure/Base/TreasureBase.prefab");
        return objBaseModel;
    }

    /// <summary>
    /// 获取样子模型
    /// </summary>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public GameObject GetTreasureLookModel(string modelName)
    {
        GameObject objBaseModel = GetModel(dicModel, "treasure/treasure", modelName, "Assets/Prefabs/Treasure/" + modelName + ".prefab");
        return objBaseModel;
    }

    #region 数据回调
    public void GetTreasureInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetTreasureInfoFail(string failMsg, Action action)
    {
    }
    #endregion
}