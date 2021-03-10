using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class GameBean 
{
    public GameStatusEnum gameStatus = GameStatusEnum.Null;
    public int gameSceneNumber = 1;

    public int levelForPower = 1;
    public int levelForPrice = 1;
    public int levelForNumber = 1;
    public int levelForUp = 1;

    public long gold = 0;
    public int maxPlayerCharacterNumber = 30;

    public float levelUpPro = 0;

    public void AddLevelUpPro(float pro)
    {
        levelUpPro += pro;
        if (levelUpPro > 1)
            levelUpPro = 1;
    }

    public void SetGameStatus(GameStatusEnum gameStatus)
    {
        this.gameStatus = gameStatus;
    }

    public GameStatusEnum GetGameStatus()
    {
        return gameStatus;
    }
    
    public bool HasEnoughGold(long goldData)
    {
        if(goldData > gold)
        {
            return false;
        }
        return true;
    }

    public long AddGold(long addGold)
    {
        gold += addGold;
        return gold;
    }

    public long PayGold(long payGold)
    {
        gold -= payGold;
        if (gold < 0)
            gold = 0;
        return gold;
    }

    public void LevelUpForPower()
    {
        levelForPower++;
    }

    public void LevelUpForPrice()
    {
        levelForPrice++;
    }

    public void LevelUpForNumber()
    {
        levelForNumber++;
    }

    public void LevelUpForLevel()
    {
        levelForUp++;
        levelUpPro = 0;
    }
}