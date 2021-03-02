using UnityEditor;
using UnityEngine;

public class AICharacterEntity : AIBaseEntity
{
    public Character character;
    public Character rivalCharacter;

    public Vector3 moveTarget;

    public virtual void InitData(Character character)
    {
        this.character = character;
        AddIntent(new IntentCharacterForMoveToRival(this));
        AddIntent(new IntentCharacterForBattle(this));
        AddIntent(new IntentCharacterForDead(this));
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
        }
        return life;
    }
}