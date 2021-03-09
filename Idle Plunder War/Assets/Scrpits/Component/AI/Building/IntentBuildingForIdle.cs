using UnityEditor;
using UnityEngine;

public class IntentBuildingForIdle : AIBaseIntent
{
    protected AIBuildingEntity buildingAI;
    protected float timeForSearchInterval = 1f;
    protected float timeForSearch = 0;
    public IntentBuildingForIdle(AIBuildingEntity aiEntity) : base(AIIntentEnum.BuildingIdle, aiEntity)
    {
        this.buildingAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {

    }

    public override void IntentActUpdate()
    {
        timeForSearch -= Time.deltaTime;
        if (timeForSearch <= 0)
        {
            HandleForSearchRival();
            timeForSearch = timeForSearchInterval;
        }
    }

    public override void IntentEntering()
    {
        timeForSearch = 0;
    }

    public override void IntentLeaving()
    {

    }

    /// <summary>
    /// 处理-视野范围
    /// </summary>
    public void HandleForSearchRival()
    {
        Vector3 centerPosition = buildingAI.transform.position;
        BuildingInfoBean buildingInfo = buildingAI.building.buildingInfoData;
        CampEnum camp = buildingAI.building.camp;
        //根据不同阵营选择不同对手
        int layer = 0;
        switch (camp)
        {
            case CampEnum.Player:
                layer = LayerInfo.Enemy;
                break;
            case CampEnum.Enemy:
                layer = LayerInfo.Player;
                break;
        }
        //射线检测视野范围内的敌人
        Collider[] listEyeCollider = RayUtil.RayToSphere(centerPosition, buildingInfo.attribute_atk_range, 1 << layer);
        if (CheckUtil.ArrayIsNull(listEyeCollider))
        {
            return;
        }
        //如果射线内又其他敌人 则检测最近的一个敌人
        float minDistance = float.MaxValue;
        Collider tempColldier = null;
        for (int i = 0; i < listEyeCollider.Length; i++)
        {
            Collider itemCollider = listEyeCollider[i];
            float tempDistance = Vector3.Distance(itemCollider.transform.position, buildingAI.transform.position);
            if (tempDistance < minDistance)
            {
                tempColldier = itemCollider;
                minDistance = tempDistance;
            }
        }
        //发现对手 前往对手
        buildingAI.rivalCharacter = tempColldier.GetComponent<Character>();
        buildingAI.ChangeIntent(AIIntentEnum.BuildingAtk);
    }
}