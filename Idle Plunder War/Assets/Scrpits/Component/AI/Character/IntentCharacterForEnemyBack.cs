﻿using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class IntentCharacterForEnemyBack : AIBaseIntent
{
    protected AICharacterForEnemyEntity characterAI;
    protected float timeForSearchInterval = 1f;
    protected float timeForSearch = 0;

    public IntentCharacterForEnemyBack(AICharacterForEnemyEntity aiEntity) : base(AIIntentEnum.CharacterEnemyBack, aiEntity)
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
            timeForSearch = timeForSearchInterval;
        }

        HandleForBack();
    }

    public override void IntentEntering()
    {
        timeForSearch = 0;
        CharacterForEnemy characterForEnemy = characterAI.character as CharacterForEnemy;
        Vector3 startPosition = characterForEnemy.positionForStart;
        characterAI.character.characterMove.SetDestination(startPosition);
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
        //射线检测视野范围内的敌人
        Collider[] listEyeCollider = RayUtil.RayToSphere(centerPosition, characterInfo.attribute_eye_range, 1 << layer);
        if (CheckUtil.ArrayIsNull(listEyeCollider))
        {
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
        //发现对手 前往对手
        characterAI.rivalCharacter = tempColldier.GetComponent<Character>();
        characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToRival);
    }


    public void HandleForBack()
    {
        if (characterAI.character.characterMove.IsAutoMoveStopForEndPath())
        {
            CharacterForEnemy characterForEnemy = characterAI.character as CharacterForEnemy;
            Vector3 startEulerAngles = characterForEnemy.eulerAnglesForStart;
            characterAI.ChangeIntent(AIIntentEnum.CharacterEnemyIdle);
            characterAI.transform.DORotate(startEulerAngles, 0.5f);
        }
    }
}