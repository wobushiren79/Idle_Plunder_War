using UnityEditor;
using UnityEngine;

public class IntentCharacterForBattle : AIBaseIntent
{
    protected AICharacterEntity characterAI;

    protected float timeForHandleInterval = 0.5f;
    protected float timeForHandle = 0;

    protected float timeForAttack = 0;
    public IntentCharacterForBattle(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterBattle, aiEntity)
    {
        this.characterAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
        timeForHandle -= Time.deltaTime;
        if (timeForHandle <= 0)
        {
            HandleForCharacterDistance();
            timeForHandle = timeForHandleInterval;
        }

        HandleForAttack();
    }

    public override void IntentEntering()
    {
        timeForHandle = 0;
        timeForAttack = 0;
        characterAI.character.characterMove.StopMove();
    }

    public override void IntentLeaving()
    {
    }

    /// <summary>
    /// 角色距离处理 如果超过距离则自动跟进
    /// </summary>
    protected void HandleForCharacterDistance()
    {
        Character character = characterAI.character;
        Character rivalCharacter = characterAI.rivalCharacter;
        //如果死亡则处理
        if (rivalCharacter == null || rivalCharacter.currentLife <= 0)
        {
            return;
        }
        float distance = Vector3.Distance(character.transform.position, rivalCharacter.transform.position);
        if (distance > character.characterInfoData.attribute_atk_range)
        {
            //如果大于攻击距离 则靠近
            Vector3 movePosition = rivalCharacter.transform.position;
            character.characterMove.SetDestination(movePosition);
        }
        else
        {
            character.characterMove.StopMove();
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
        timeForAttack = characterAI.character.characterInfoData.attribute_atk_interval;

        int damage = characterAI.character.currentAtk;

        int enemyLife;
        if (characterAI.rivalCharacter == null || characterAI.rivalCharacter.currentLife <= 0)
        {
            enemyLife = 0;
        }
        else
        {
            enemyLife = characterAI.rivalCharacter.characterAI.UnderAttack(damage);
        }
        //如果角色已经死亡
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