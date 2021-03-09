using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AtkTypeManager : BaseManager
{
    //攻击模型数据
    public Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();

    public GameObject GetAtkModel(string modelName)
    {
        GameObject objModel = GetModel(dicModel, "model/atk", modelName);
        return objModel;
    }

}