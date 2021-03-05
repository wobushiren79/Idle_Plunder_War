using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : BaseManager
{
    public GameInputActions inputActions;

    public virtual void Awake()
    {
        inputActions = new GameInputActions();
        inputActions.Player.TargetMove.Enable();
        inputActions.Player.CameraMove.Enable();
    }

    /// <summary>
    /// 获取目标移动数据
    /// </summary>
    /// <returns></returns>
    public InputAction GetTargetMoveData()
    {
        return inputActions.Player.TargetMove;
    }

    /// <summary>
    /// 获取摄像头移动数据
    /// </summary>
    /// <returns></returns>
    public InputAction GetCameraMoveData()
    {
        return inputActions.Player.CameraMove;
    }
}