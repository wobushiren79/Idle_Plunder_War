using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToTreasure : AIBaseIntent
{
    protected AICharacterEntity characterAI;

    protected float timeForSearchInterval = 0.2f;
    protected float timeForSearch = 0;

    public IntentCharacterForMoveToTreasure(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterMoveToTreasure, aiEntity)
    {
        this.characterAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
        timeForSearch -= Time.deltaTime;
        if (timeForSearch <= 0)
        {
            HandleForSearchBattle();
            timeForSearch = timeForSearchInterval;
        }

        HandleForArrive();
    }

    public override void IntentEntering()
    {
        characterAI.character.characterAnim.PlayWalk();
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



    /// <summary>
    /// 处理-是否进入战斗范围
    /// </summary>
    protected void HandleForSearchBattle()
    {
        Vector3 centerPosition = characterAI.transform.position;
        CharacterInfoBean characterInfo = characterAI.character.characterInfoData;

        //检测是否进入战斗范围
        Collider[] listAtkCollider = RayUtil.RayToSphere(centerPosition, characterInfo.attribute_atk_range, 1 << LayerInfo.Treasure);
        if (CheckUtil.ArrayIsNull(listAtkCollider))
            return;
        //选取距离最近的单位
        Collider tempColldier = listAtkCollider[0];
        characterAI.ChangeIntent(AIIntentEnum.CharacterOpenTreasure);
    }
}