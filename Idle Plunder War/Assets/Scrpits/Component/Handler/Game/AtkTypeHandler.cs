using UnityEditor;
using UnityEngine;

public class AtkTypeHandler : BaseHandler<AtkTypeHandler,AtkTypeManager>
{

    public void AtkTarget(AtkTypeEnum atkType, Character atkCharacter, Character underAtkCharacter)
    {
        atkCharacter.characterAnim.PlayAttack();
        switch (atkType)
        {
            case AtkTypeEnum.Melee:
                HandleForMelee();
                break;
            case AtkTypeEnum.RemoteArcherySingle:
                HandleForRemoteArcherySingle(underAtkCharacter.transform.position);
                break;
            case AtkTypeEnum.RemoteArcherySingleTrace:
                HandleForRemoteArcherySingleTrace();
                break;
        }
    }


    public void HandleForMelee()
    {

    }

    public void HandleForRemoteArcherySingle(Vector3 targetPosition)
    {

    }

    public void HandleForRemoteArcherySingleTrace()
    {

    }
}