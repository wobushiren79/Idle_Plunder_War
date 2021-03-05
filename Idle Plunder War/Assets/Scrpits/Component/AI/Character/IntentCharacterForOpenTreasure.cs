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

        int damage = characterAI.character.currentAtk;
        int treasureLife = characterAI.targetTreasure.ChangeLife(-damage);
        characterAI.character.characterAnim.PlayAttack();
        //如果宝箱已经打开
        if (treasureLife <= 0)
        {
            GameHandler.Instance.EndGame();
        }
    }
}