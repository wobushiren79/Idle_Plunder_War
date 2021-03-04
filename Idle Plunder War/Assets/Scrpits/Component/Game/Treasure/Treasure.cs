using UnityEditor;
using UnityEngine;

public class Treasure : BaseMonoBehaviour
{
    public TreasureInfoBean treasureInfo;

    public int currentLife;
    public int currentMaxLife;
    public void SetData(TreasureInfoBean treasureInfo)
    {
        this.treasureInfo = treasureInfo;
        currentLife = treasureInfo.attribute_life;
        currentMaxLife = treasureInfo.attribute_life;
    }

    public int ChangeLife(int changeLife)
    {
        currentLife += changeLife;
        if (currentLife < 0)
            currentLife = 0;
        return currentLife;
    }
}