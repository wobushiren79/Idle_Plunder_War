/*
* FileName: LevelInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-03-10:12:03 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class LevelInfoBean : BaseBean
{
    public int level;
    public long pre_gold;
    public string data;
    public float pro;

    public void GetData(out float value)
    {
        value = float.Parse(data);
    }

    public void GetData(out float value1, out float value2)
    {
        float[] listData = StringUtil.SplitBySubstringForArrayFloat(data, ',');
        value1 = listData[0];
        value2 = listData[1];
    }


}