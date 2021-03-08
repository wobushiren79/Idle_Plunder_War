using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IntentCharacterForOpenTreasure : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    protected float timeForOpen;
    public IntentCharacterForOpenTreasure(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterOpenTreasure, aiEntity)
    {
        this.characterAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
        HandleForOpen();
    }

    public override void IntentEntering()
    {
        characterAI.character.characterMove.StopMove();
        timeForOpen = 0;
    }

    public override void IntentLeaving()
    {

    }

    /// <summary>
    /// 攻击处理
    /// </summary>
    protected void HandleForOpen()
    {
        timeForOpen -= Time.deltaTime;
        if (timeForOpen > 0)
            return;
        timeForOpen = characterAI.character.characterInfoData.attribute_atk_interval;

        if (characterAI.targetTreasure != null)
        {
            AtkTypeHandler.Instance.AtkTarget(characterAI.character.characterInfoData.GetAtkType(), characterAI.character, characterAI.targetTreasure);
        }
        //如果宝箱已经打开
        if (characterAI.targetTreasure != null && characterAI.targetTreasure.currentLife <= 0)
        {
            TreasureHandler.Instance.manager.ClearAllTreasure();
            GameHandler.Instance.EndGame();
        }
    }
}