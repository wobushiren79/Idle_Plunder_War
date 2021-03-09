using UnityEditor;
using UnityEngine;

public class BuildingHandler : BaseHandler<BuildingHandler, BuildingManager>
{

    /// <summary>
    /// 创建宝藏
    /// </summary>
    /// <param name="treasureInfo"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public Building CreateBuilding(long buildingId, Vector3 position, Vector3 eulerAngles)
    {
        BuildingInfoBean buildingInfo = manager.GetBuildingInfo(buildingId);
        Building building = CreateBuilding(buildingInfo, position, eulerAngles);
        building.camp = CampEnum.Enemy;
        building.buildingAI.ChangeIntent(AIIntentEnum.BuildingIdle);
        return building;
    }

    /// <summary>
    /// 创建宝藏
    /// </summary>
    /// <param name="treasureInfo"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public Building CreateBuilding(BuildingInfoBean buildingInfo, Vector3 position,Vector3 eulerAngles)
    {
        //获取模型
        GameObject objBaseModel = manager.GetBuildingBaseModel();
        GameObject objLookModel = manager.GetBuildingLookModel(buildingInfo.model_name);
        //实例化
        GameObject objChacater = Instantiate(gameObject, objBaseModel, position);
        Instantiate(objChacater, objLookModel);
        objChacater.transform.eulerAngles = eulerAngles;
        //设置数据
        Building building = objChacater.GetComponent<Building>();
        building.SetData(buildingInfo);
        //记录进list
        manager.AddBuilding(building);

        return building;
    }
}