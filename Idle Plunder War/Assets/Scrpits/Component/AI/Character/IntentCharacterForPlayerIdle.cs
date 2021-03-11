using UnityEditor;
using UnityEngine;

public class IntentCharacterForPlayerIdle : AIBaseIntent
{
    protected AICharacterForPlayerEntity characterAI;
    public IntentCharacterForPlayerIdle(AICharacterForPlayerEntity aiEntity) : base(AIIntentEnum.CharacterPlayerIdle, aiEntity)
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
        AICharacterEntity aiCharacter = aiEntity as AICharacterEntity;

        //分配对手
        aiCharacter.rivalCharacter = CharacterHandler.Instance.manager.DistributeRival(characterAI.character);
        if (aiCharacter.rivalCharacter != null)
        {
            //移动到对手位置
            characterAI.moveTarget = aiCharacter.rivalCharacter.transform.position;
            characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToRival);
            return;
        }
        aiCharacter.targetBuilding = BuildingHandler.Instance.manager.DistributeBuilding(characterAI.character);
        if (aiCharacter.targetBuilding != null)
        {
            //往建筑移动
            characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToBuilding);
            return;
        }      
        //前往宝藏
        characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToTreasure);
    }

    public override void IntentLeaving()
    {
    }
}