using Pathfinding;
using UnityEditor;
using UnityEngine;

public class CharacterMove : BaseMonoBehaviour
{
    protected AIPath aiPath;

    private void Awake()
    {
        if (aiPath == null)
            aiPath = GetComponent<AIPath>();
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        aiPath.maxSpeed = speed;
    }

    /// <summary>
    /// 自动移动
    /// </summary>
    /// <param name="position">目的地</param>
    public void SetDestination(Vector3 targetPosition)
    {
        StopMove();
        aiPath.maxAcceleration = float.MaxValue;
        aiPath.destination = targetPosition;
        aiPath.SearchPath();
        aiPath.onSearchPath = () =>
        {
            aiPath.canMove = true;
        };
    }

    /// <summary>
    /// 停止寻路
    /// </summary>
    public void StopMove()
    {
        aiPath.canMove = false;
    }

    /// <summary>
    /// 关闭寻路
    /// </summary>
    public void ClosePath()
    {
        aiPath.enabled = false;
        //rvoController.enabled = false;
    }

    /// <summary>
    /// 自动寻路是否停止
    /// </summary>
    /// <returns></returns>
    public bool IsAutoMoveStop()
    {
        //有路径，到达目的地或者与最终目的地相隔
        //aiPath.reachedDestination（终点）
        //aiPath.reachedEndOfPath (路径的末端 可能没有达到终点 用于寻路不可到达的地点)
        if (!aiPath.pathPending && aiPath.hasPath && aiPath.reachedDestination)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsAutoMoveStopForEndPath()
    {
        //有路径，到达目的地或者与最终目的地相隔
        //aiPath.reachedDestination（终点）
        //aiPath.reachedEndOfPath (路径的末端 可能没有达到终点 用于寻路不可到达的地点)
        if (!aiPath.pathPending && aiPath.hasPath && aiPath.reachedEndOfPath)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsAutoMoveStop(float remainingDistance)
    {
        if (!aiPath.pathPending && aiPath.hasPath && aiPath.remainingDistance <= remainingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}