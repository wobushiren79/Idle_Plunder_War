using System;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class AtkArchery : BaseMonoBehaviour
{
    protected BoxCollider archeryCollider;
    public void Awake()
    {
        archeryCollider = GetComponent<BoxCollider>();
    }
    //重力
    protected float g = 4f;
    protected float speed = 6f;
    //目标坐标
    public Vector3 targetPosition;

    private float verticalSpeed;
    private Vector3 moveDirection;

    private float angleSpeed;
    private float angle;
    private float time;

    protected bool isFire = false;

    public int damage;
    //阵营
    public CampEnum camp;
    //开火目标
    public GameBaseItem objFire;

    public void Update()
    {
        if (!isFire)
            return;
        time += Time.deltaTime;
        float test = verticalSpeed - g * time;
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.up * test * Time.deltaTime, Space.World);
        float testAngle = -angle + angleSpeed * time;
        transform.eulerAngles = new Vector3(testAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!isFire)
            return;
        GameBaseItem baseItem ;
        if (collision.gameObject.layer == LayerInfo.Building
            || collision.gameObject.layer == LayerInfo.Treasure)
        {
             baseItem = collision.transform.GetComponentInParent<GameBaseItem>();
        }
        else
        {
             baseItem = collision.transform.GetComponent<GameBaseItem>();
        }
 
        if (baseItem != null)
        {
            if(baseItem.camp == camp)
            {
                //射中友军
                return;
            }
            else
            {
                isFire = false;
                transform.SetParent(collision.transform);
                baseItem.UnderAttack(objFire, damage);
                Destroy(gameObject);
                return;
            }
        }
        //射中地面
        isFire = false;
        archeryCollider.isTrigger = true;
    }


    /// <summary>
    /// 发射弓箭
    /// </summary>
    /// <param name="targetPosition"></param>
    public void FireArchery(GameBaseItem objFire, CampEnum camp,int damage, Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        this.camp = camp;
        this.damage = damage;
        this.objFire = objFire;
        
        float tempDistance = Vector3.Distance(transform.position, targetPosition);
        float tempTime = tempDistance / speed;
        float riseTime, downTime;
        riseTime = downTime = tempTime / 2;
        verticalSpeed = g * riseTime;
        transform.LookAt(targetPosition);

        float tempTan = verticalSpeed / speed;
        double hu = Math.Atan(tempTan);
        angle = (float)(180 / Math.PI * hu);
        transform.eulerAngles = new Vector3(-angle, transform.eulerAngles.y, transform.eulerAngles.z);
        angleSpeed = angle / riseTime;

        moveDirection = targetPosition - transform.position;

        isFire = true;

        StartCoroutine(CoroutineForDestory(5));
    }

    public IEnumerator CoroutineForDestory(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}