using UnityEditor;
using UnityEngine;

public class RayUtil
{

    /// <summary>
    /// 屏幕点击射线检测
    /// </summary>
    /// <param name="isCollider"></param>
    /// <param name="hit"></param>
    public static void RayToScreenPoint(out bool isCollider, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isCollider = Physics.Raycast(ray, out hit);
    }

    /// <summary>
    /// 屏幕点击射线检测
    /// </summary>
    /// <param name="maxDistance"></param>
    /// <param name="layer"></param>
    /// <param name="hitInfo"></param>
    public static void RayToScreenPoint(float maxDistance,int layer, out RaycastHit hitInfo)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast( ray, out  hitInfo, maxDistance, layer);
    }

    /// <summary>
    /// 射线-球体
    /// </summary>
    /// <param name="centerPosition"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Collider[] RayToSphere(Vector3 centerPosition, float radius, int layer)
    {
        return Physics.OverlapSphere(centerPosition, radius, layer);
    }
}