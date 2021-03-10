using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGameEnd : BaseUIComponent
{
    public Button ui_BtNext;

    public override void Awake()
    {
        base.Awake();
        if (ui_BtNext)
            ui_BtNext.onClick.AddListener(NextScene);
    }

    public void NextScene()
    {
        Action actionForStartGame = () =>
        {
            GameHandler.Instance.StartGame();
        };
        GameHandler.Instance.InitGame(actionForStartGame);
    }
}