using UnityEditor;
using UnityEngine;

public class IntentCharacterForRest : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    public IntentCharacterForRest(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterRest, aiEntity)
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
        characterAI.character.characterAnim.PlayWin();
        characterAI.character.characterAnim.PlayIdle();
        characterAI.character.characterMove.StopMove();
    }

    public override void IntentLeaving()
    {
    }
}