using UnityEditor;
using UnityEngine;
using System.Collections;
public class IntentCharacterForDead : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    public IntentCharacterForDead(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterDead, aiEntity)
    {
        this.characterAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
    }

    public override void IntentEntering()
    {
        characterAI.character.SetCharacterDead();
        CharacterHandler.Instance.manager.RemoveCharacter(characterAI.character);
        
    }

    public override void IntentLeaving()
    {

    }
}