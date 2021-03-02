using UnityEditor;
using UnityEngine;

public enum AIIntentEnum 
{
    //敌方闲置
    CharacterEnemyIdle,
    //敌方返回
    CharacterEnemyBack,
    //友方闲置
    CharacterPlayerIdle,
    //角色前往目的地
    CharacterMoveToRival,
    //角色战斗模式
    CharacterBattle,
    //角色死亡
    CharacterDead,
}