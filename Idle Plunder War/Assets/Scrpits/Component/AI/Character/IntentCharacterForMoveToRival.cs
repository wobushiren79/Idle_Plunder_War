using UnityEditor;
using UnityEngine;

public class IntentCharacterForMoveToRival : AIBaseIntent
{
    protected AICharacterEntity characterAI;
    protected float timeForSearchInterval = 1f;
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
        HandleForSearchRival();
    }

    public override void IntentEntering()
    {

    }

    public override void IntentLeaving()
    {

    }

    /// <summary>
    /// 处理-搜寻对手
    /// </summary>
    public void HandleForSearchRival()
    {
        timeForSearch -= Time.deltaTime;
        if (timeForSearch <= 0)
        {
            GoToRival();
            timeForSearch = timeForSearchInterval;
        }
    }

    /// <summary>
    /// 前往对手
    /// </summary>
    protected void GoToRival()
    {
        Vector3 centerPosition = characterAI.transform.position;
        RayUtil.RayToSphere(centerPosition, 5, LayerInfo.Player);

        Vector3 targetPosition = characterAI.rivalCharacter.transform.position;
        characterAI.character.characterMove.SetDestination(targetPosition);
    }
}