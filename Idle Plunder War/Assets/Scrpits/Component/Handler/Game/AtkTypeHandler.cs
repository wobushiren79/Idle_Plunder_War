using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class AtkTypeHandler : BaseHandler<AtkTypeHandler, AtkTypeManager>
{

    public void AtkTarget(AtkTypeEnum atkType, Character atkCharacter, Character underAtkCharacter)
    {
        atkCharacter.characterAnim.PlayAttack();
        switch (atkType)
        {
            case AtkTypeEnum.Melee:
                HandleForMelee(atkCharacter.gameObject, atkCharacter.currentAtk, underAtkCharacter);
                break;
            case AtkTypeEnum.RemoteArcherySingle:
                HandleForRemoteArcherySingle(atkCharacter.gameObject, atkCharacter.characterCamp, atkCharacter.currentAtk, atkCharacter.atkPosition.position, underAtkCharacter.transform.position + new Vector3(0, 0.5f, 0));
                break;
            case AtkTypeEnum.RemoteArcherySingleTrace:
                HandleForRemoteArcherySingleTrace();
                break;
        }
    }

    public void AtkTarget(AtkTypeEnum atkType, Building atkBuilding, Character underAtkCharacter)
    {
        switch (atkType)
        {
            case AtkTypeEnum.Melee:
                HandleForMelee(atkBuilding.gameObject, atkBuilding.currentAtk, underAtkCharacter);
                break;
            case AtkTypeEnum.RemoteArcherySingle:
                HandleForRemoteArcherySingle(atkBuilding.gameObject, atkBuilding.camp, atkBuilding.currentAtk, atkBuilding.atkPosition.position, underAtkCharacter.transform.position + new Vector3(0, 0.5f, 0));
                break;
            case AtkTypeEnum.RemoteArcherySingleTrace:
                HandleForRemoteArcherySingleTrace();
                break;
        }
    }
    public void AtkTarget(AtkTypeEnum atkType, Character atkCharacter, Building underAtkBuilding)
    {
        atkCharacter.characterAnim.PlayAttack();
        switch (atkType)
        {
            case AtkTypeEnum.Melee:
                HandleForMelee(atkCharacter.gameObject, atkCharacter.currentAtk, underAtkBuilding);
                break;
            case AtkTypeEnum.RemoteArcherySingle:
                HandleForRemoteArcherySingle(atkCharacter.gameObject, atkCharacter.characterCamp, atkCharacter.currentAtk, atkCharacter.atkPosition.position, underAtkBuilding.transform.position + new Vector3(0, 0.5f, 0));
                break;
            case AtkTypeEnum.RemoteArcherySingleTrace:
                HandleForRemoteArcherySingleTrace();
                break;
        }
    }

    public void HandleForMelee(GameObject objAtk, int damage, Character underAtkCharacter)
    {
        underAtkCharacter.characterAI.UnderAttack(objAtk, damage);
    }
    public void HandleForMelee(GameObject objAtk, int damage, Building underAtkBuilding)
    {
        underAtkBuilding.UnderAttack(objAtk, damage);
    }

    public void HandleForRemoteArcherySingle(GameObject objAtk, CampEnum camp, int damage, Vector3 startPosition, Vector3 targetPosition)
    {
        GameObject objModel = manager.GetAtkModel("AtkArchery_1");
        GameObject objArchery = Instantiate(gameObject, objModel, startPosition);

        AtkArchery atkArchery = objArchery.GetComponent<AtkArchery>();
        atkArchery.FireArchery(objAtk, camp, damage, targetPosition);
    }

    public void HandleForRemoteArcherySingleTrace()
    {
        GameObject objModel = manager.GetAtkModel("AtkArchery_1");
        GameObject objArchery = Instantiate(gameObject, objModel);
    }
}