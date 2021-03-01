using UnityEditor;
using UnityEngine;

public class IntentCharacterForEnemyIdle : AIBaseIntent
{
    protected AICharacterForEnemyEntity characterAI;
    public IntentCharacterForEnemyIdle(AICharacterForEnemyEntity aiEntity) : base(AIIntentEnum.CharacterEnemyIdle, aiEntity)
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

}