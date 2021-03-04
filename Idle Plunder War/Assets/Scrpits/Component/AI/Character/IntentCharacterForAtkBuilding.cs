using UnityEditor;
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