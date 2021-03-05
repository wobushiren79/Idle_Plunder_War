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
    //角色前往宝藏处
    CharacterMoveToTreasure,
    //角色前往点击处
    CharacterMoveToTouch,
    //角色前往建筑
    CharacterMoveToBuilding,
    //角色打开宝箱
    CharacterOpenTreasure,
    //角色战斗模式
    CharacterBattle,
    //角色攻击塔
    CharacterAtkBuilding,
    //角色死亡
    CharacterDead,
    //角色休息
    CharacterRest,
}