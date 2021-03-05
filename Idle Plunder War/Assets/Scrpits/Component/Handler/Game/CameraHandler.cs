using UnityEditor;
using UnityEngine;

public class CameraHandler : BaseHandler<CameraHandler, CameraManager>
{
    protected Vector3 sceneCenterPosition = Vector3.zero;
    protected float speedForZoom = 5;
    protected float speedForMove = 5;
    protected float maxAroundY = 70;
    protected float minAroundY = 20;

    /// <summary>
    /// 围绕场景中心点旋转摄像头
    /// </summary>
    /// <param name="direction"></param>
    public void RotateCameraAroundXZ(int direction)
    {
        RotateCameraAroundXZ((float)direction * 50);
    }
    public void RotateCameraAroundXZ(float rotateOffset)
    {
        Camera camera = manager.mainCamera;
        camera.transform.RotateAround(sceneCenterPosition, Vector3.up, rotateOffset * Time.deltaTime * speedForMove);
    }

    public void RotateCameraAroundY(int direction)
    {
        RotateCameraAroundY((float)direction);
    }
    public void RotateCameraAroundY(float rotateOffset)
    {
        Camera camera = manager.mainCamera;
        Vector3 eulerAngles = camera.transform.eulerAngles;
        float tempAngles = rotateOffset * Time.deltaTime * speedForMove;
        if (rotateOffset > 0 && eulerAngles.x + tempAngles >= maxAroundY)
            return;
        if (rotateOffset < 0 && eulerAngles.x + tempAngles <= minAroundY)
            return;
        camera.transform.RotateAround(sceneCenterPosition, camera.transform.right, tempAngles);
    }

    public void ZoomCamera(float zoomOffset)
    {
        Camera camera = manager.mainCamera;
        manager.SetCameraFieldOfView(zoomOffset * Time.deltaTime * speedForZoom + camera.fieldOfView);
    }

    public void MoveCamera(Vector3 moveOffset)
    {
        Camera camera = manager.mainCamera;
        camera.transform.position += moveOffset * speedForMove * Time.deltaTime;
    }
}