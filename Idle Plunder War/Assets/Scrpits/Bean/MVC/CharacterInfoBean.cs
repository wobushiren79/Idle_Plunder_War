/*
* FileName: CharacterInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-10:29:33 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class CharacterInfoBean : BaseBean
{
    //模型名称
    public string model_name;
    //属性
    public float attribute_speed;
    public float attribute_atk;
    public float attribute_atk_range;
    public int attribute_life;
    //比重
    public int weight;

}