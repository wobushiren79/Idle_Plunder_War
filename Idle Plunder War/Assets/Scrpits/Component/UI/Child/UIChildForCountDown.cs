using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIChildForCountDown : BaseUIChildComponent<UIGameMain>
{
    public List<Sprite> listCountDownSprite = new List<Sprite>();
    public Image ivCountDown;

    public void SetCountDownTime(int time)
    {
        if (time >= listCountDownSprite.Count)
        {
            time = listCountDownSprite.Count-1;
        }
        Sprite spTime = listCountDownSprite[time];
        ivCountDown.sprite = spTime;
    }
}