using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class GameBean 
{
    public GameStatusEnum gameStatus = GameStatusEnum.Null;

    public void SetGameStatus(GameStatusEnum gameStatus)
    {
        this.gameStatus = gameStatus;
    }

    public GameStatusEnum GetGameStatus()
    {
        return gameStatus;
    }
}