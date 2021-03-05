using System.Linq;
using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToRival : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    protected float timeForSearchInterval = 0.2f;
    protected float timeForSearch = 0;
    public IntentCharacterForMoveToRival(AICharacterEntity aiEntity) : base(AIIntentEnum.CharacterMoveToRival, aiEntity)
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
            HandleForSearchRival();
            HandleForSearchBattle();
            timeForSearch = timeForSearchInterval;
        }
    }

    public override void IntentEntering()
    {
        characterAI.character.characterAnim.PlayWalk();
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
        Vector3 centerPosition = characterAI.transform.position;
        CharacterInfoBean characterInfo = characterAI.character.characterInfoData;
        CharacterCampEnum characterCamp = characterAI.character.characterCamp;
        //根据不同阵营选择不同对手
        //射线检测视野范围内的敌人
        Collider[] listEyeCollider = null;
        switch (characterCamp)
        {
            case CharacterCampEnum.Player:
                listEyeCollider = RayUtil.RayToSphere(centerPosition, characterInfo.attribute_eye_range, 1 << LayerInfo.Enemy | 1 << LayerInfo.Building);
                break;
            case CharacterCampEnum.Enemy:
                listEyeCollider = RayUtil.RayToSphere(centerPosition, characterInfo.attribute_eye_range, 1 << LayerInfo.Player);
                break;
        }
        if (CheckUtil.ArrayIsNull(listEyeCollider))
        {
            //玩家
            if (characterCamp == CharacterCampEnum.Player)
            {
                //如果没有其他对手 则继续前往之前的对手
                if (characterAI.rivalCharacter == null || characterAI.rivalCharacter.currentLife <= 0)
                {
                    //如果敌人已经死亡
                    characterAI.ChangeIntent(AIIntentEnum.CharacterPlayerIdle);
                    return;
                }
            }
            //敌人
            else if (characterCamp == CharacterCampEnum.Enemy)
            {
                //如果没有其他对手 则继续前往之前的对手
                if (characterAI.rivalCharacter == null || characterAI.rivalCharacter.currentLife <= 0)
                {
                    //如果敌人已经死亡
                    characterAI.ChangeIntent(AIIntentEnum.CharacterEnemyBack);
                    return;
                }
            }
            //如果敌人没有死亡
            Vector3 targetPosition = characterAI.rivalCharacter.transform.position;
            characterAI.character.characterMove.SetDestination(targetPosition);
            return;
        }
        //如果射线内又其他敌人 则检测最近的一个敌人前往
        float minDistance = float.MaxValue;
        Collider tempColldier = null;
        for (int i = 0; i < listEyeCollider.Length; i++)
        {
            Collider itemCollider = listEyeCollider[i];
            float tempDistance = Vector3.Distance(itemCollider.transform.position, characterAI.transform.position);
            if (tempDistance < minDistance)
            {
                tempColldier = itemCollider;
                minDistance = tempDistance;
            }
        }
        //如果是敌人
        characterAI.rivalCharacter = tempColldier.GetComponent<Character>();
        if (characterAI.rivalCharacter != null)
        {
            Vector3 rivalPosition = characterAI.rivalCharacter.transform.position;
            characterAI.character.characterMove.SetDestination(rivalPosition);
            return;
        }
        //如果是建筑
        characterAI.targetBuilding = tempColldier.GetComponentInParent<Building>();
        if (characterAI.targetBuilding != null)
        {
            characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToBuilding);
            return;
        }
    }

    /// <summary>
    /// 处理-是否进入战斗范围
    /// </summary>
    protected void HandleForSearchBattle()
    {
        Vector3 centerPosition = characterAI.transform.position;
        CharacterInfoBean characterInfo = characterAI.character.characterInfoData;
        CharacterCampEnum characterCamp = characterAI.character.characterCamp;
        //根据不同阵营选择不同对手
        int layer = 0;
        switch (characterCamp)
        {
            case CharacterCampEnum.Player:
                layer = LayerInfo.Enemy;
                break;
            case CharacterCampEnum.Enemy:
                layer = LayerInfo.Player;
                break;
        }
        //检测是否进入战斗范围
        Collider[] listAtkCollider = RayUtil.RayToSphere(centerPosition, characterInfo.attribute_atk_range, 1 << layer);
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
        characterAI.rivalCharacter = tempColldier.GetComponent<Character>();
        characterAI.ChangeIntent(AIIntentEnum.CharacterBattle);
    }
}