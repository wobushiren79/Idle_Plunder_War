using UnityEditor;
using UnityEngine;

public class AICharacterEntity : AIBaseEntity
{
    public Character character;
    public Character rivalCharacter;

    public Vector3 moveTarget;

    public virtual void InitData(Character character)
    {
        this.character =  character;
        AddIntent(new IntentCharacterForMoveToRival(this));
    }
}