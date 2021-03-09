using UnityEditor;
using UnityEngine;

public class GameBaseItem : BaseMonoBehaviour
{
    public CampEnum camp;
    public int currentMaxLife;
    public int currentLife;
    public int currentAtk;

    /// <summary>
    /// 攻击点
    /// </summary>
    protected Transform _atkPosition;
    public Transform atkPosition
    {
        get
        {
            if (_atkPosition == null)
            {
                _atkPosition = CptUtil.GetCptInChildrenByName<Transform>(gameObject, "AtkPosition");
            }
            return _atkPosition;
        }
    }

    /// <summary>
    /// 收到攻击 
    /// </summary>
    /// <param name="objAtk"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public virtual int UnderAttack(GameBaseItem atk, int damage)
    {
        int life = ChangeLife(-damage);
        return life;
    }

    /// <summary>
    /// 改变生命值
    /// </summary>
    /// <param name="changeLife"></param>
    /// <returns></returns>
    public int ChangeLife(int changeLife)
    {
        currentLife += changeLife;
        if (currentLife < 0)
            currentLife = 0;
        return currentLife;
    }
}