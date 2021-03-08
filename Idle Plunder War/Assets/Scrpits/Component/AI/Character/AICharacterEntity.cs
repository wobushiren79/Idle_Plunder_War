using UnityEditor;
using UnityEngine;

public class AICharacterEntity : AIBaseEntity
{
    public Character character;
    public Character rivalCharacter;
    public Treasure targetTreasure;
    public Building targetBuilding;

    public Vector3 moveTarget;

    public virtual void InitData(Character character)
    {
        this.character = character;
        AddIntent(new IntentCharacterForMoveToRival(this));
        AddIntent(new IntentCharacterForBattle(this));
        AddIntent(new IntentCharacterForDead(this));
        AddIntent(new IntentCharacterForRest(this));
    }

    /// <summary>
    /// 收到攻击
    /// </summary>
    /// <param name="objAtk"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int UnderAttack(GameObject objAtk, int damage)
    {
        int life = character.ChangeLife(-damage);
        if (life <= 0)
        {
            ChangeIntent(AIIntentEnum.CharacterDead);
            //如果是敌人 增加金币
            if (character.characterCamp == CampEnum.Enemy)
            {
                GameBean gameData = GameHandler.Instance.manager.gameData;
                LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
                levelInfo.GetData(out float levelData);
                gameData.AddGold((long)(character.characterInfoData.price * levelData));
            }
        }
        else
        {
            //反击
            if (objAtk == null)
                return life;
            if (currentIntent.aiIntent == AIIntentEnum.CharacterPlayerIdle
                || currentIntent.aiIntent == AIIntentEnum.CharacterEnemyIdle
                || currentIntent.aiIntent == AIIntentEnum.CharacterEnemyBack)
            {
                //如果是人
                Character targetCharacter = objAtk.GetComponent<Character>();
                if (targetCharacter != null)
                {
                    rivalCharacter = targetCharacter;
                    ChangeIntent(AIIntentEnum.CharacterMoveToRival);
                }
                else
                {             
                    //如果是建筑
                    Building targetBuilding = objAtk.GetComponent<Building>();
                    if (targetBuilding != null)
                    {
                        this.targetBuilding = targetBuilding;
                        ChangeIntent(AIIntentEnum.CharacterMoveToBuilding);
                    }
                }
            }
        }
        //受伤
        character.Injured();
        return life;
    }
}