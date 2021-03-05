using UnityEditor;
using UnityEngine;

public class TreasureHandler : BaseHandler<TreasureHandler, TreasureManager>
{

    public Treasure CreateTreasure(long id, Vector3 position, Vector3 eulerAngles)
    {
        TreasureInfoBean treasureInfo = manager.GetTreasureInfo(id);
        return CreateTreasure(treasureInfo, position, eulerAngles);
    }

    /// <summary>
    /// 创建宝藏
    /// </summary>
    /// <param name="treasureInfo"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public Treasure CreateTreasure(TreasureInfoBean treasureInfo, Vector3 position, Vector3 eulerAngles)
    {
        //获取模型
        GameObject objBaseModel = manager.GetTreasureBaseModel();
        GameObject objLookModel = manager.GetTreasureLookModel(treasureInfo.model_name);
        //实例化
        GameObject objChacater = Instantiate(gameObject, objBaseModel, position);
        Instantiate(objChacater, objLookModel);
        objChacater.transform.eulerAngles = eulerAngles;
        //设置数据
        Treasure treasure = objChacater.GetComponent<Treasure>();
        treasure.SetData(treasureInfo);
        //记录进list
        manager.SetTreasure(treasure);

        return treasure;
    }

}