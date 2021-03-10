/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-11:07:11 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SceneInfoBean : BaseBean
{
    public float character_build_interval;

    public string enemy_data;

    public string building_data;

    public float enemy_range;

    public string treasure_data;

    public string player_position;
    public string camera_position;

    public string player_character;

    public List<long> GetPlayerCharacter()
    {
        List<long> listData = new List<long>();
        if (CheckUtil.StringIsNull(player_character))
            return listData;
        long[] dataArray = StringUtil.SplitBySubstringForArrayLong(player_character, ',');
        listData = dataArray.ToList();
        return listData;
    }

    public EnemyTreasureData GetTreasureData()
    {
        EnemyTreasureData enemyBaseData = JsonUtil.FromJson<EnemyTreasureData>(treasure_data);
        return enemyBaseData;
    }

    public void SetTreasureData(EnemyTreasureData treasureData)
    {
        treasure_data = JsonUtil.ToJson(treasureData);
    }

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

    public void SetPlayerPosition(Vector3 postion, Vector3 angle)
    {
        player_position = postion.x + "," + postion.y + "," + postion.z + "|" + angle.x + "," + angle.y + "," + angle.z;
    }
    public void SetCameraPosition(Vector3 postion, Vector3 angle)
    {
        camera_position = postion.x + "," + postion.y + "," + postion.z + "|" + angle.x + "," + angle.y + "," + angle.z;
    }

    public void GetPlayerPosition(out Vector3 postion, out Vector3 angle)
    {
        GetPosition(player_position, out postion, out angle);
    }
    public void GetCameraPosition(out Vector3 postion, out Vector3 angle)
    {
        GetPosition(camera_position, out postion, out angle);
    }

    protected void GetPosition(string data, out Vector3 postion, out Vector3 angle)
    {
        if (CheckUtil.StringIsNull(data))
        {
            postion = Vector3.zero;
            angle = Vector3.zero;
            return;
        }
        string[] dataList = StringUtil.SplitBySubstringForArrayStr(data, '|');
        string positionStr = dataList[0];
        string angleStr = dataList[1];
        float[] positionList = StringUtil.SplitBySubstringForArrayFloat(positionStr, ',');
        float[] angleList = StringUtil.SplitBySubstringForArrayFloat(angleStr, ',');
        postion = new Vector3(positionList[0], positionList[1], positionList[2]);
        angle = new Vector3(angleList[0], angleList[1], angleList[2]);
    }
}