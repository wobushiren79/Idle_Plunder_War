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

    public List<EnemyCharacterData> GetListEnemyData()
    {
        DataStorageListBean<EnemyCharacterData> enemyBaseData = JsonUtil.FromJson<DataStorageListBean<EnemyCharacterData>>(enemy_data);
        return enemyBaseData.listData;
    }

    public void SetListEnemyData(List<EnemyCharacterData> listData)
    {
        DataStorageListBean<EnemyCharacterData> handBean = new DataStorageListBean<EnemyCharacterData>();
        handBean.listData = listData;
        enemy_data = JsonUtil.ToJson(handBean);
    }
}