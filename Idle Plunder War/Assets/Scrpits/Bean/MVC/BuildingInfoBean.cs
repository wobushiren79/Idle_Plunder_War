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
    public int attribute_atk;
    public int attribute_life;
    public int price;

}