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
        HandleForArrive();
    }

    public override void IntentEntering()
    {
        Treasure treasure = TreasureHandler.Instance.manager.GetTreasure();
        characterAI.targetTreasure = treasure;
        characterAI.character.characterMove.SetDestination(treasure.transform.position);
    }

    public override void IntentLeaving()
    {

    }

    public void HandleForArrive()
    {
        //检测是否到达目的地
        if (characterAI.character.characterMove.IsAutoMoveStopForEndPath())
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterOpenTreasure);
        }
    }
}