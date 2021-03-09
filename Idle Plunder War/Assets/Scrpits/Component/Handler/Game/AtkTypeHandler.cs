using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class AtkTypeHandler : BaseHandler<AtkTypeHandler, AtkTypeManager>
{

    public void AtkTarget(AtkTypeEnum atkType, GameBaseItem atk, GameBaseItem underAtk)
    {
        if (atk is Character)
        {
            Character atkCharacter = atk as Character;
            atkCharacter.characterAnim.PlayAttack();
        }
        Vector3 offsetPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        switch (atkType)
        {
            case AtkTypeEnum.Melee:
                HandleForMelee(atk, underAtk);
                break;
            case AtkTypeEnum.RemoteArcherySingle:

                HandleForRemoteArcherySingle(atk,  atk.atkPosition.position, underAtk.transform.position + offsetPosition);
                break;
            case AtkTypeEnum.RemoteArcherySingleTrace:
                HandleForRemoteArcherySingleTrace();
                break;
        }
    }

    public void HandleForMelee(GameBaseItem atk, GameBaseItem underAtk)
    {
        underAtk.UnderAttack(atk, atk.currentAtk);
    }

    public void HandleForRemoteArcherySingle(GameBaseItem atk,  Vector3 startPosition, Vector3 targetPosition)
    {
        GameObject objModel = manager.GetAtkModel("AtkArchery_1");
        GameObject objArchery = Instantiate(gameObject, objModel, startPosition);

        AtkArchery atkArchery = objArchery.GetComponent<AtkArchery>();
        atkArchery.FireArchery(atk, atk.camp, atk.currentAtk, targetPosition);
    }

    public void HandleForRemoteArcherySingleTrace()
    {
        GameObject objModel = manager.GetAtkModel("AtkArchery_1");
        GameObject objArchery = Instantiate(gameObject, objModel);
    }
}