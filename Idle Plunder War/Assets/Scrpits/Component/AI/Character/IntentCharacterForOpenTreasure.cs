using UnityEditor;
using UnityEngine;

public class IntentCharacterForOpenTreasure : AIBaseIntent
{
    protected AICharacterEntity characterAI;

    public IntentCharacterForOpenTreasure(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterOpenTreasure, aiEntity)
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
    }

    public override void IntentLeaving()
    {

    }

    public void HandleForArrive()
    {
      
    }
}