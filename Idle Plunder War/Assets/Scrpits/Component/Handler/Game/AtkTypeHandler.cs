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
        Vector3 offsetPosition = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        switch (atkType)
        {
            case AtkTypeEnum.Melee:
                HandleForMelee(atk, underAtk);
                break;
            case AtkTypeEnum.RemoteArcherySingle:
                HandleForRemoteArcherySingle(atk, atk.atkPosition.position, underAtk.transform.position + offsetPosition);
                break;
            case AtkTypeEnum.RemoteArcherySingleTrace:
                HandleForRemoteArcherySingleTrace();
                break;
            case AtkTypeEnum.RemoteArcherySingleRange:
                HandleForRemoteArcherySingleRange(atk, atk.atkPosition.position, underAtk.transform.position + offsetPosition);
                break;
        }
    }

    public void HandleForMelee(GameBaseItem atk, GameBaseItem underAtk)
    {
        underAtk.UnderAttack(atk, atk.currentAtk);
    }

    public void HandleForRemoteArcherySingle(GameBaseItem atk, Vector3 startPosition, Vector3 targetPosition)
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

    public void HandleForRemoteArcherySingleRange(GameBaseItem atk, Vector3 startPosition, Vector3 targetPosition)
    {
        for (int i = 0; i < 10; i++)
        {
            HandleForRemoteArcherySingle(atk, startPosition + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), targetPosition + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
        }
    }
}