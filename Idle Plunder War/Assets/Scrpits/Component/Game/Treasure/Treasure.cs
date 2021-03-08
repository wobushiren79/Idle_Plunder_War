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


}