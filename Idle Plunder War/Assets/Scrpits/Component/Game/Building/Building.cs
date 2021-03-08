﻿using System;
using UnityEditor;
using UnityEngine;

public class Building : GameBaseItem
{
    public BuildingInfoBean buildingInfoData;
    public AIBuildingEntity buildingAI;

    private void Awake()
    {
        buildingAI = CptUtil.AddCpt<AIBuildingEntity>(gameObject);
        buildingAI.InitData(this);
    }

    public void SetData(BuildingInfoBean buildingInfoData)
    {
        this.buildingInfoData = buildingInfoData;
        currentLife = buildingInfoData.attribute_life;
        currentMaxLife = buildingInfoData.attribute_life;
        currentAtk = buildingInfoData.attribute_atk;
    }


    /// <summary>
    /// 收到攻击 
    /// </summary>
    /// <param name="objAtk"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public override int UnderAttack(GameBaseItem objAtk, int damage)
    {
        int life = base.UnderAttack(objAtk, damage);
        if (life <= 0)
        {
            //增加金币
            GameBean gameData = GameHandler.Instance.manager.gameData;
            LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
            levelInfo.GetData(out float levelData);
            gameData.AddGold((long)(buildingInfoData.price * levelData));
            //死亡
            Destroy(gameObject);
        }
        return life;
    }
}