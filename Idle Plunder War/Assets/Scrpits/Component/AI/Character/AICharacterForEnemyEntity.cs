using UnityEditor;
using UnityEngine;

public class AICharacterForEnemyEntity : AICharacterEntity
{

    public override void InitData(Character character)
    {
        base.InitData(character);
        //增加意图
        AddIntent(new IntentCharacterForEnemyIdle(this));
    }
}