using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMain : BaseUIComponent, UIViewForLevelUp.ICallBack, UIViewForAttributeUp.ICallBack
{
    public UIChildForCountDown ui_CountDown;
    public Text ui_TvGold;
    public UIViewForLevelUp ui_LevelUp;


    public UIViewForAttributeUp ui_Power;
    public UIViewForAttributeUp ui_Number;
    public UIViewForAttributeUp ui_Price;

    public override void Awake()
    {
        base.Awake();
        if (ui_Power)
            ui_Power.SetCallBack(this);
        if (ui_Number)
            ui_Number.SetCallBack(this);
        if (ui_Price)
            ui_Price.SetCallBack(this);
        if (ui_LevelUp)
            ui_LevelUp.SetCallBack(this);
        RefreshUI();
    }

    private void Update()
    {
        SetTimeCountDown();
        SetGold();
        SetLevelUpPro();
    }

    public override void RefreshUI()
    {
        base.RefreshUI();
        SetPowerInfo();
        SetNumberInfo();
        SetPriceInfo();
    }

    public void SetLevelUpPro()
    {
        if (ui_LevelUp)
        {
            GameBean gameData = GameHandler.Instance.manager.gameData;
            ui_LevelUp.SetLevelUpPro(gameData.levelForUp, gameData.levelUpPro);
        }
    }

    public void SetGold()
    {
        if (ui_TvGold != null)
            ui_TvGold.text = GameHandler.Instance.manager.gameData.gold + "";
    }

    public void SetTimeCountDown()
    {
        if (ui_CountDown != null)
            ui_CountDown.SetCountDownTime((int)GameHandler.Instance.timeCountdownForCreatePlayer);
    }

    public void SetPowerInfo()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPower(gameData.levelForPower);
        ui_Power.SetData(gameData.levelForPower, levelInfo.pre_gold);
    }

    public void SetNumberInfo()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForNumber(gameData.levelForNumber);
        ui_Number.SetData(gameData.levelForNumber, levelInfo.pre_gold);
    }

    public void SetPriceInfo()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
        ui_Price.SetData(gameData.levelForPrice, levelInfo.pre_gold);
    }

    public void OnClickForPower()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPower(gameData.levelForPower);
        if (gameData.HasEnoughGold(levelInfo.pre_gold))
        {
            gameData.PayGold(levelInfo.pre_gold);
            gameData.LevelUpForPower();
            CharacterHandler.Instance.RefreshPlayerCharacter();
            RefreshUI();
        }
    }

    public void OnClickForNumber()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForNumber(gameData.levelForNumber);
        if (gameData.HasEnoughGold(levelInfo.pre_gold))
        {
            gameData.PayGold(levelInfo.pre_gold);
            gameData.LevelUpForNumber();
            CharacterHandler.Instance.RefreshPlayerCharacter();
            RefreshUI();
        }
    }

    public void OnClickForPrice()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
        if (gameData.HasEnoughGold(levelInfo.pre_gold))
        {
            gameData.PayGold(levelInfo.pre_gold);
            gameData.LevelUpForPrice();
            CharacterHandler.Instance.RefreshPlayerCharacter();
            RefreshUI();
        }
    }

    #region 升级按钮回调
    public void OnClickForLevelUp()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        if (gameData.levelUpPro < 1)
            return;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForLevelUp(gameData.levelForUp);
        levelInfo.GetData(out float levelData);
        gameData.AddGold((long)levelData);
        gameData.LevelUpForLevel();
        RefreshUI();
    }

    public void OnClickForAttributeAdd(UIViewForAttributeUp view)
    {
        if (view == ui_Power)
        {
            OnClickForPower();
        }
        else if (view == ui_Number)
        {
            OnClickForNumber();
        }
        else if (view == ui_Price)
        {
            OnClickForPrice();
        }
    }
    #endregion
}