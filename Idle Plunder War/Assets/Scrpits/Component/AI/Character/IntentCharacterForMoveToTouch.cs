using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToTouch : AIBaseIntent
{
    protected AICharacterEntity characterAI;

    public IntentCharacterForMoveToTouch(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterMoveToTouch, aiEntity)
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
        characterAI.character.characterAnim.PlayWalk();
        Vector3 moveTarget = characterAI.moveTarget+ new Vector3(Random.Range(-1,1),0, Random.Range(-1, 1));
        characterAI.moveTarget = moveTarget;
        characterAI.character.characterMove.SetDestination(characterAI.moveTarget);
    }

    public override void IntentLeaving()
    {

    }

    public void HandleForArrive()
    {
        //检测是否到达目的地
        if (characterAI.character.characterMove.IsAutoMoveStopForEndPath())
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterPlayerIdle);
        }
    }

}