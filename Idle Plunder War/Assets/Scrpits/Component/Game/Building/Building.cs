using System;
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
            buildingAI.ChangeIntent(AIIntentEnum.BuildingDead);
        }
        return life;
    }

    /// <summary>
    /// 设置角色死亡
    /// </summary>
    public void SetBuildingDead()
    {
        currentLife = 0;
        gameObject.layer = LayerInfo.Dead;
        Transform tfNew = CptUtil.GetCptInChildrenByName<Transform>(gameObject, "New");
        Transform tfOld = CptUtil.GetCptInChildrenByName<Transform>(gameObject, "Old");
        if (tfNew)
            tfNew.gameObject.SetActive(false);
        if (tfOld)
            tfOld.gameObject.SetActive(true);
    }
}