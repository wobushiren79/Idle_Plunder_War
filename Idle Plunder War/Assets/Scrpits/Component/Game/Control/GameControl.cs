using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameControl : BaseMonoBehaviour
{
    public void Update()
    {
        HandleForTargetMove();
    }

    public void HandleForTargetMove()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        InputAction inputAction = InputHandler.Instance.manager.GetTargetMoveData();
        float data = inputAction.ReadValue<float>();
        if (data == 0)
            return;
        switch (inputAction.phase)
        {
            case InputActionPhase.Started:
                RayUtil.RayToScreenPoint(100, 1 << LayerInfo.Ground, out RaycastHit hitInfo);
                if (hitInfo.collider != null)
                    CharacterHandler.Instance.MovePlayerCharacter(hitInfo.point);
                break;
        }
    }
}