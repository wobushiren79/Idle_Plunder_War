using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToTreasure : AIBaseIntent
{
    protected AICharacterEntity characterAI;

    public IntentCharacterForMoveToTreasure(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterMoveToTreasure, aiEntity)
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
        //检测是否到达目的地
        if (characterAI.character.characterMove.IsAutoMoveStopForEndPath())
        {

        }
    }
}