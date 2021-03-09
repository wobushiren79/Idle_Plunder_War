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
}