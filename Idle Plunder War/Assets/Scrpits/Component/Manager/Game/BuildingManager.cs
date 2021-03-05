using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingManager : BaseManager,IBuildingInfoView
{

    protected BuildingInfoController controllerForBuildingInfo;
    //宝藏模型数据
    public Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();
    //宝藏信息数据
    public Dictionary<long, BuildingInfoBean> dicBuildingInfo = new Dictionary<long, BuildingInfoBean>();

    public List<Building> listBuilding = new List<Building>();
    private void Awake()
    {
        InitAllBuildingInfo();
    }

    /// <summary>
    /// 初始化所有数据
    /// </summary>
    public void InitAllBuildingInfo()
    {
        if (controllerForBuildingInfo == null)
        {
            controllerForBuildingInfo = new BuildingInfoController(this, this);
        }
        Action<List<BuildingInfoBean>> callBack = (listData) =>
        {
            dicBuildingInfo.Clear();
            for (int i = 0; i < listData.Count; i++)
            {
                BuildingInfoBean itemData = listData[i];
                dicBuildingInfo.Add(itemData.id, itemData);
            }
        };
        controllerForBuildingInfo.GetAllBuildingInfoData(callBack);
    }

    public BuildingInfoBean GetBuildingInfo(long buildingId)
    {
        if (dicBuildingInfo == null)
            return null;
        if(dicBuildingInfo.TryGetValue(buildingId,out BuildingInfoBean value))
        {
            return value;
        }
        return null;
    }

    public void AddBuilding(Building building)
    {
        listBuilding.Add(building);
    }

    public void RemoveBuilding(Building building)
    {
        if (listBuilding.Contains(building))
            listBuilding.Remove(building);
    }

    /// <summary>
    /// 获取基础模型
    /// </summary>
    /// <param name="characterCamp"></param>
    /// <returns></returns>
    public GameObject GetBuildingBaseModel()
    {
        GameObject objBaseModel = GetModel(dicModel, "building/base", "BuildingBase", "Assets/Prefabs/Building/Base/BuildingBase.prefab");
        return objBaseModel;
    }

    /// <summary>
    /// 获取样子模型
    /// </summary>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public GameObject GetBuildingLookModel(string modelName)
    {
        GameObject objBaseModel = GetModel(dicModel, "building/building", modelName, "Assets/Prefabs/Building/" + modelName + ".prefab");
        return objBaseModel;
    }

    /// <summary>
    /// 清除所有角色
    /// </summary>
    public void ClearAllCharacter()
    {
        CptUtil.RemoveChildsByActive(gameObject);
        listBuilding.Clear();
    }

    public void ClearAllCharacterInEditor()
    {
        CptUtil.RemoveChildsByActiveInEditor(gameObject);
        listBuilding.Clear();
    }

    #region 数据回掉
    public void GetBuildingInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetBuildingInfoFail(string failMsg, Action action)
    {

    }
    #endregion
}