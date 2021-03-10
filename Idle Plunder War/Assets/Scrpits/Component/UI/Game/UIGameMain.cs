using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMain : BaseUIComponent, UIViewForLevelUp.ICallBack
{
    public UIChildForCountDown ui_CountDown;
    public Text ui_TvGold;
    public UIViewForLevelUp ui_LevelUp;

    public Button ui_BtPowerAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtPriceAdd;

    public Text ui_TvPowerLevel;
    public Text ui_TvNumberLevel;
    public Text ui_TvPriceLevel;

    public Text ui_TvPowerMoney;
    public Text ui_TvNumberMoney;
    public Text ui_TvPriceMoney;

    public override void Awake()
    {
        base.Awake();
        if (ui_BtPowerAdd)
            ui_BtPowerAdd.onClick.AddListener(OnClickForPower);
        if (ui_BtNumberAdd)
            ui_BtNumberAdd.onClick.AddListener(OnClickForNumber);
        if (ui_BtPriceAdd)
            ui_BtPriceAdd.onClick.AddListener(OnClickForPrice);
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
        ui_TvPowerLevel.text = "Lv." + gameData.levelForPower;
        ui_TvPowerMoney.text = levelInfo.pre_gold + "";
    }

    public void SetNumberInfo()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForNumber(gameData.levelForNumber);
        ui_TvNumberLevel.text = "Lv." + gameData.levelForNumber;
        ui_TvNumberMoney.text = levelInfo.pre_gold + "";
    }

    public void SetPriceInfo()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
        ui_TvPriceLevel.text = "Lv." + gameData.levelForPrice;
        ui_TvPriceMoney.text = levelInfo.pre_gold + "";
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
    #endregion
}