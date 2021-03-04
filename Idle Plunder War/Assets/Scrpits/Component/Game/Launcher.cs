using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        Action actionForStartGame = () =>
        {
            GameHandler.Instance.StartGame();
        };
        GameHandler.Instance.InitGame(actionForStartGame);
    }

    void Update()
    {
        
    }

}
