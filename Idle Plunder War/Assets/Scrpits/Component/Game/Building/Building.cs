using System;
using UnityEditor;
using UnityEngine;

public class Building : BaseMonoBehaviour
{
    public BuildingInfoBean buildingInfoData;
    public AIBuildingEntity buildingAI;
    public int currentLife;
    public int currentMaxLife;
    public int currentAtk;
    public CampEnum camp;

    /// <summary>
    /// 攻击点
    /// </summary>
    protected Transform _atkPosition;
    public Transform atkPosition
    {
        get
        {
            if (_atkPosition == null)
            {
                _atkPosition = CptUtil.GetCptInChildrenByName<Transform>(gameObject, "AtkPosition");
            }
            return _atkPosition;
        }
    }


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

    public int ChangeLife(int changeLife)
    {
        currentLife += changeLife;
        if (currentLife < 0)
            currentLife = 0;
        return currentLife;
    }


    /// <summary>
    /// 收到攻击 
    /// </summary>
    /// <param name="objAtk"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int UnderAttack(GameObject objAtk, int damage)
    {
        int life = ChangeLife(-damage);
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