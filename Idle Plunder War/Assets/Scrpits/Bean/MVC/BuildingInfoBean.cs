/*
* FileName: BuildingInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-15:14:57 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class BuildingInfoBean : BaseBean
{
    public string model_name;
    //建造类型
    public int building_type;
    public int attribute_atk;
    public int attribute_life;
    public float attribute_atk_range;
    public float attribute_atk_interval;
    public int price;

    public int atk_type;

    public AtkTypeEnum GetAtkType()
    {
        return (AtkTypeEnum)atk_type;
    }

    public BuildingTypeEnum GetBuildingType()
    {
        return (BuildingTypeEnum)building_type;
    }

}