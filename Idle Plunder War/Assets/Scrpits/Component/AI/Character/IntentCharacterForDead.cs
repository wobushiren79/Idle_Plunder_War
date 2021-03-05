using UnityEditor;
using UnityEngine;

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
        AnimatorStateInfo animatorInfo = characterAI.character.characterAnim.characterAnim.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.normalizedTime >= 0.95f)
        {
            GameObject.Destroy(characterAI.gameObject);
        }
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