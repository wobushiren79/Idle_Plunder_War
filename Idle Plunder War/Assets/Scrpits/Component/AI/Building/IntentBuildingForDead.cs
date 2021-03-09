using UnityEditor;
using UnityEngine;
public class IntentBuildingForDead : AIBaseIntent
{
    protected AIBuildingEntity buildingAI;
    public IntentBuildingForDead(AIBuildingEntity aiEntity) : base(AIIntentEnum.CharacterDead, aiEntity)
    {
        this.buildingAI = aiEntity;
    }

    public override void IntentActFixUpdate()
    {
    }

    public override void IntentActUpdate()
    {
    }

    public override void IntentEntering()
    {
        buildingAI.building.SetBuildingDead();
    }

    public override void IntentLeaving()
    {

    }
}