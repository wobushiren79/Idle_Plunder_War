using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToTreasure : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    protected float timeForSearchInterval = 0.2f;
    protected float timeForSearch = 0;
    public IntentCharacterForMoveToTreasure(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterMoveToTreasure, aiEntity)
    {
        this.characterAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
        timeForSearch -= Time.deltaTime;
        if (timeForSearch <= 0)
        {
            HandleForSearchBattle();
            timeForSearch = timeForSearchInterval;
        }
        HandleForArrive();
    }

    public override void IntentEntering()
    {
        characterAI.character.characterAnim.PlayWalk();
        Treasure treasure = TreasureHandler.Instance.manager.GetTreasure();
        characterAI.targetTreasure = treasure;
        characterAI.character.characterMove.SetDestination(treasure.transform.position);
    }

    public override void IntentLeaving()
    {

    }

    public void HandleForArrive()
    {
        //检测是否到达目的地
        if (characterAI.character.characterMove.IsAutoMoveStopForEndPath())
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterOpenTreasure);
        }
    }

    /// <summary>
    /// 处理-是否进入战斗范围
    /// </summary>
    protected void HandleForSearchBattle()
    {
        float distance = Vector3.Distance(characterAI.character.transform.position, characterAI.targetTreasure.transform.position);
        if( distance <= characterAI.character.characterInfoData.attribute_atk_range)
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterOpenTreasure);
        }
    }

}