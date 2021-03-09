using UnityEditor;
using UnityEngine;

public class IntentBuildingForAtk : AIBaseIntent
{
    protected AIBuildingEntity buildingAI;

    protected float timeForHandleInterval = 0.5f;
    protected float timeForHandle = 0;

    protected float timeForAttack = 0;
    public IntentBuildingForAtk(AIBuildingEntity aiEntity) : base(AIIntentEnum.BuildingAtk, aiEntity)
    {
        this.buildingAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
        timeForHandle -= Time.deltaTime;
        if (timeForHandle <= 0)
        {
            HandleForRival();
            timeForHandle = timeForHandleInterval;
        }
        HandleForAttack();
    }

    public override void IntentEntering()
    {
        timeForHandle = 0;
        timeForAttack = 0;
    }

    public override void IntentLeaving()
    {
    }

    public void HandleForRival()
    {
        if (buildingAI.rivalCharacter == null || buildingAI.rivalCharacter.currentLife <= 0)
        {
            buildingAI.ChangeIntent(AIIntentEnum.BuildingIdle);
        }
        else
        {
            float distance = Vector3.Distance(buildingAI.transform.position, buildingAI.rivalCharacter.characterAI.transform.position);
            if (distance-0.5f > buildingAI.building.buildingInfoData.attribute_atk_range)
            {
                buildingAI.ChangeIntent(AIIntentEnum.BuildingIdle);
            }
        }
    }

    /// <summary>
    /// 攻击处理
    /// </summary>
    protected void HandleForAttack()
    {
        timeForAttack -= Time.deltaTime;
        if (timeForAttack > 0)
            return;
        timeForAttack = buildingAI.building.buildingInfoData.attribute_atk_interval;


        int enemyLife = 0;
        if (buildingAI.rivalCharacter == null || buildingAI.rivalCharacter.currentLife <= 0)
        {
            enemyLife = 0;
        }
        else
        {

            AtkTypeHandler.Instance.AtkTarget(buildingAI.building.buildingInfoData.GetAtkType(), buildingAI.building, buildingAI.rivalCharacter);

            enemyLife = buildingAI.rivalCharacter.currentLife;
        }


        //如果角色已经死亡
        if (enemyLife <= 0)
        {
            buildingAI.ChangeIntent(AIIntentEnum.BuildingIdle);
        }
    }
}