using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMain : BaseUIComponent
{
    public Text ui_TvCountDown;

    public Button ui_BtPowerAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtPriceAdd;

    public override void Awake()
    {
        base.Awake();
        if (ui_BtPowerAdd)
            ui_BtPowerAdd.onClick.AddListener(OnClickForPower);
        if (ui_BtNumberAdd)
            ui_BtNumberAdd.onClick.AddListener(OnClickForSpeed);
        if (ui_BtPriceAdd)
            ui_BtPriceAdd.onClick.AddListener(OnClickForPrice);
    }

    public void Update()
    {
        RefreshUI();
    }

    public override void RefreshUI()
    {
        base.RefreshUI();
        SetTimeCountDown();
    }

    public void SetTimeCountDown()
    {
        if (ui_TvCountDown != null)
            ui_TvCountDown.text = GameHandler.Instance.timeCountdownForCreatePlayer + "";
    }

    public void OnClickForPower()
    {

    }

    public void OnClickForSpeed()
    {

    }

    public void OnClickForPrice()
    {

    }
}