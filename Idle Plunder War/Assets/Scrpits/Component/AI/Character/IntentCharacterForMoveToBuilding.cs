using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToBuilding : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    protected float timeForSearchInterval = 0.5f;
    protected float timeForSearch = 0;
    public IntentCharacterForMoveToBuilding(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterMoveToBuilding, aiEntity)
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
        Vector3 buildingPosition = characterAI.targetBuilding.transform.position;
        characterAI.character.characterMove.SetDestination(buildingPosition);
    }

    public override void IntentLeaving()
    {

    }

    public void HandleForArrive()
    {
        if (characterAI.targetBuilding == null || characterAI.targetBuilding.currentLife <= 0)
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterPlayerIdle);
            return;
        }

        //检测是否到达目的地
        if (characterAI.character.characterMove.IsAutoMoveStopForEndPath())
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterAtkBuilding);
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
        Collider[] listAtkCollider = RayUtil.RayToSphere(centerPosition, characterInfo.attribute_atk_range, 1 << LayerInfo.Building);
        if (CheckUtil.ArrayIsNull(listAtkCollider))
            return;
        //选取距离最近的单位
        float minDistance = float.MaxValue;
        Collider tempColldier = null;
        for (int i = 0; i < listAtkCollider.Length; i++)
        {
            Collider itemCollider = listAtkCollider[i];
            float tempDistance = Vector3.Distance(itemCollider.transform.position, characterAI.transform.position);
            if (tempDistance < minDistance)
            {
                tempColldier = itemCollider;
                minDistance = tempDistance;
            }
        }
        characterAI.targetBuilding = tempColldier.GetComponentInParent<Building>();
        characterAI.ChangeIntent(AIIntentEnum.CharacterAtkBuilding);
    }

}