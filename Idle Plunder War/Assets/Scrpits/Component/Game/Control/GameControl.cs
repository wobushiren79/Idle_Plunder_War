using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameControl : BaseMonoBehaviour
{
    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2) 

    public void Update()
    {
        HandleForTargetMove();
        HandleForCameraMove();
        HandleForCameraZoom();
    }

    public void HandleForTargetMove()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        if (EventSystem.current.IsPointerOverGameObject() || gameData.GetGameStatus() != GameStatusEnum.InGame)
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

    public void HandleForCameraMove()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        InputAction inputAction = InputHandler.Instance.manager.GetCameraMoveData();
        Vector2 data = inputAction.ReadValue<Vector2>();
        if (data != Vector2.zero)
        {
            CameraHandler.Instance.MoveCamera(new Vector3(-data.x,0,-data.y));
        }
        ////双点触摸， 水平移动
        //if (Input.touchCount == 2 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Moved && Input.GetTouch(1).phase == UnityEngine.TouchPhase.Moved)
        //{
        //    //没有点到UI时
        //    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
        //        var deltaposition_0 = Input.GetTouch(0).deltaPosition;
        //        var deltaposition_1 = Input.GetTouch(1).deltaPosition;
        //        float horizontal = -deltaposition_1.y;
        //        float vertical = -deltaposition_1.x;
        //        CameraHandler.Instance.MoveCamera(new Vector3(-vertical, 0, -horizontal));
        //    }
        //}
    }

    public void HandleForCameraZoom()
    {
        //如果触碰到了UI
        if (Input.touchCount != 2
            || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
            || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
            return;
        //多点触摸, 放大缩小  
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        //第2点刚开始接触屏幕, 只记录，不做处理  
        if (newTouch2.phase == UnityEngine.TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差，为正表示放大手势， 为负表示缩小手势  
        float offset = newDistance - oldDistance;

        //进行缩放
        float scaleFactor = -offset;

        CameraHandler.Instance.ZoomCamera(scaleFactor);

        //记住最新的触摸点，下次使用  
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }
}