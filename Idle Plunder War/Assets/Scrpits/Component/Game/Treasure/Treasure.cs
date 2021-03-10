using UnityEditor;
using UnityEngine;

public class Treasure : GameBaseItem
{
    public TreasureInfoBean treasureInfo;

    public void SetData(TreasureInfoBean treasureInfo)
    {
        this.treasureInfo = treasureInfo;
        currentLife = treasureInfo.attribute_life;
        currentMaxLife = treasureInfo.attribute_life;
    }

    public override int UnderAttack(GameBaseItem atk, int damage)
    {
        EffectHandler.Instance.PlayEffect("effect_baoxiang_baojinbi", transform.position, 2);
        return base.UnderAttack(atk, damage);
    }
}