/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-11:07:11 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class SceneInfoBean : BaseBean
{
    public float character_build_interval;

    public string enemy_data;

    public string building_data;

    public float enemy_range;

    public long treasure_id;

    public List<EnemyCharacterData> GetListEnemyData()
    {
        DataStorageListBean<EnemyCharacterData> enemyBaseData = JsonUtil.FromJson<DataStorageListBean<EnemyCharacterData>>(enemy_data);
        if (enemyBaseData == null)
            return new List<EnemyCharacterData>();
        return enemyBaseData.listData;
    }

    public void SetListEnemyData(List<EnemyCharacterData> listData)
    {
        DataStorageListBean<EnemyCharacterData> handBean = new DataStorageListBean<EnemyCharacterData>();
        handBean.listData = listData;
        enemy_data = JsonUtil.ToJson(handBean);
    }

    public List<EnemyBuildingData> GetListBuildingData()
    {
        DataStorageListBean<EnemyBuildingData> enemyBaseData = JsonUtil.FromJson<DataStorageListBean<EnemyBuildingData>>(building_data);
        if (enemyBaseData == null)
            return new List<EnemyBuildingData>();
        return enemyBaseData.listData;
    }

    public void SetListBuildingData(List<EnemyBuildingData> listData)
    {
        DataStorageListBean<EnemyBuildingData> handBean = new DataStorageListBean<EnemyBuildingData>();
        handBean.listData = listData;
        building_data = JsonUtil.ToJson(handBean);
    }
}