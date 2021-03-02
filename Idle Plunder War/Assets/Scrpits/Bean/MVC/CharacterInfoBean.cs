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
    //ģ������
    public string model_name;
    //����
    public float attribute_speed;
    public int attribute_atk;
    public float attribute_atk_range;
    public float attribute_atk_interval;
    public float attribute_eye_range;
    public int attribute_life;
    //����
    public int weight;

}