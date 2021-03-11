using DG.Tweening;
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
        atk.transform.LookAt(gameObject.transform.position);
        GameBean gameData = GameHandler.Instance.manager.gameData;
        gameData.AddGold(damage * treasureInfo.price);
        EffectHandler.Instance.PlayEffect("effect_baoxiang_baojinbi", transform.position, 2);
        transform.DOKill();
        transform.transform.localScale = Vector3.one;
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetEase(Ease.OutBack).From();
        return base.UnderAttack(atk, damage);
    }
}