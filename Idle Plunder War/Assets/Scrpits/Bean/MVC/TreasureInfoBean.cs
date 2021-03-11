/*
* FileName: TreasureInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-09:59:26 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class TreasureInfoBean : BaseBean
{
    public string model_name;
    public int attribute_life;
    public long price = 1;
}