using UnityEditor;
using UnityEngine;

public class CameraManager : BaseManager
{
    protected Camera _mainCamera;

    protected float maxOrthographicSize = 100;
    protected float minOrthographicSize = 20;

    public Camera mainCamera 
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }

    public void SetCameraFieldOfView(float fieldOfView)
    {
        if (fieldOfView > maxOrthographicSize)
            fieldOfView = maxOrthographicSize;
        else if (fieldOfView < minOrthographicSize)
            fieldOfView = minOrthographicSize;
        mainCamera.fieldOfView = fieldOfView;
    }

    public void SetCameraFieldOfViewForPro(float pro)
    {
        float fieldOfView = (maxOrthographicSize - minOrthographicSize) * pro + minOrthographicSize;
        SetCameraFieldOfView(fieldOfView);
    }
}