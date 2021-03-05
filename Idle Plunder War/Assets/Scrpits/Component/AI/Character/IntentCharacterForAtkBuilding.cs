﻿using UnityEditor;
using UnityEngine;

public class IntentCharacterForAtkBuilding : AIBaseIntent
{
    protected AICharacterEntity characterAI;

    protected float timeForAttack = 0;

    public IntentCharacterForAtkBuilding(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterAtkBuilding, aiEntity)
    {
        this.characterAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
        HandleForAttack();
    }

    public override void IntentEntering()
    {
        characterAI.character.characterAnim.PlayIdle();
        timeForAttack = 0;
        characterAI.character.characterMove.StopMove();
    }

    public override void IntentLeaving()
    {
    }


    /// <summary>
    /// 攻击处理
    /// </summary>
    protected void HandleForAttack()
    {
        timeForAttack -= Time.deltaTime;
        if (timeForAttack > 0)
            return;
        timeForAttack = characterAI.character.characterInfoData.attribute_atk_interval;

        //面朝对手
        characterAI.character.transform.LookAt(characterAI.rivalCharacter.transform.position);

        int damage = characterAI.character.currentAtk;

        int enemyLife;
        if (characterAI.targetBuilding == null || characterAI.targetBuilding.currentLife <= 0)
        {
            enemyLife = 0;
        }
        else
        {
            enemyLife = characterAI.targetBuilding.UnderAttack(damage);
        }
        characterAI.character.characterAnim.PlayAttack();
        //如果建筑已经死亡
        if (enemyLife <= 0)
        {
            switch (characterAI.character.characterCamp)
            {
                case CharacterCampEnum.Player:
                    characterAI.ChangeIntent(AIIntentEnum.CharacterPlayerIdle);
                    break;
                case CharacterCampEnum.Enemy:
                    characterAI.ChangeIntent(AIIntentEnum.CharacterEnemyBack);
                    break;
            }
        }
    }
}