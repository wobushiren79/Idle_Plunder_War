using UnityEditor;
using UnityEngine;

public class AIBuildingEntity : AIBaseEntity
{

    public Building building;
    public Character rivalCharacter;

    public virtual void InitData(Building building)
    {
        this.building = building;
        AddIntent(new IntentBuildingForAtk(this));
        AddIntent(new IntentBuildingForIdle(this));
    }
}