using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingManager : BaseManager, IBuildingInfoView
{

    protected BuildingInfoController controllerForBuildingInfo;
    //建筑模型数据
    public Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();
    //建筑信息数据
    public Dictionary<long, BuildingInfoBean> dicBuildingInfo = new Dictionary<long, BuildingInfoBean>();

    public List<Building> listBuilding = new List<Building>();
    private void Awake()
    {
        InitAllBuildingInfo();
    }


    /// <summary>
    /// 分配建筑
    /// </summary>
    /// <param name="character"></param>
    public Building DistributeBuilding(Character character)
    {
        float minDistance = float.MaxValue;
        Building targetBuilding = null;
        for (int i = 0; i < listBuilding.Count; i++)
        {
            Building itemBuilding = listBuilding[i];
            if (itemBuilding.buildingInfoData.GetBuildingType() != BuildingTypeEnum.Atk || itemBuilding.currentLife <= 0)
            {
                continue;
            }
            //选择距离最近的对手
            float tempDis = Vector3.Distance(character.transform.position, itemBuilding.transform.position);
            if (tempDis < minDistance)
            {
                targetBuilding = itemBuilding;
                minDistance = tempDis;
            }
        }
        return targetBuilding;
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
        if (dicBuildingInfo.TryGetValue(buildingId, out BuildingInfoBean value))
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

    public void ClearAllBuilding()
    {
        CptUtil.RemoveChildsByActive(gameObject);
        listBuilding.Clear();
    }

    public void ClearAllBuildingInEditor()
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