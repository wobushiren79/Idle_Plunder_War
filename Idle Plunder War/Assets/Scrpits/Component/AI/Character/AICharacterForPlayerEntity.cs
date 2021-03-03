using System;
using UnityEditor;
using UnityEngine;

public class AICharacterForPlayerEntity : AICharacterEntity
{
    public override void InitData(Character character)
    {
        base.InitData(character);
        //增加意图
        AddIntent(new IntentCharacterForPlayerIdle(this));
        AddIntent(new IntentCharacterForMoveToTreasure(this));
        AddIntent(new IntentCharacterForOpenTreasure(this));
    }


}