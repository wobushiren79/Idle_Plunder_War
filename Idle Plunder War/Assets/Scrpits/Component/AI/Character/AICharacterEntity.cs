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
    /// <param name="damage"></param>
    public int UnderAttack(int damage)
    {
        int life = character.ChangeLife(-damage);
        if (life <= 0)
        {
            ChangeIntent(AIIntentEnum.CharacterDead);
            //如果是敌人 增加金币
            if (character.characterCamp == CharacterCampEnum.Enemy)
            {
                GameBean gameData = GameHandler.Instance.manager.gameData;
                LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
                levelInfo.GetData(out float levelData);
                gameData.AddGold((long)(character.characterInfoData.price * levelData));
            }
        }
        return life;
    }
}