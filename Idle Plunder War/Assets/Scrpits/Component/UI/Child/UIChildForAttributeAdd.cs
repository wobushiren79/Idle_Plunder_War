using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIChildForAttributeAdd : BaseUIChildComponent<UIGameMain>
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtGoldPriceAdd;

    public TextMeshProUGUI ui_TvTitleSpeed;
    public Text ui_TvSpeedLevel;
    public TextMeshProUGUI ui_TvSpeedMoney;

    public TextMeshProUGUI ui_TvTitleNumber;
    public Text ui_TvNumberLevel;
    public TextMeshProUGUI ui_TvNumberMoney;

    public TextMeshProUGUI ui_TvTitleGoldPrice;
    public Text ui_TvGoldPriceLevel;
    public TextMeshProUGUI ui_TvGoldPriceMoney;


    public Color color_MoneyOutline;
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (ui_BtSpeedAdd)
            ui_BtSpeedAdd.onClick.AddListener(OnClickForAddSpeed);
        if (ui_BtNumberAdd)
            ui_BtNumberAdd.onClick.AddListener(OnClickForAddNumber);
        if (ui_BtGoldPriceAdd)
            ui_BtGoldPriceAdd.onClick.AddListener(OnClickForAddGoldPrice);

        if (ui_TvTitleGoldPrice)
        {
            ui_TvTitleGoldPrice.outlineColor = Color.white;
            ui_TvTitleGoldPrice.text = TextHandler.Instance.manager.GetTextById(1);
            SetUnderlayOffsetY(ui_TvTitleGoldPrice);
        }
        if (ui_TvTitleSpeed)
        {
            ui_TvTitleSpeed.outlineColor = Color.white;
            ui_TvTitleSpeed.text = TextHandler.Instance.manager.GetTextById(2);
            SetUnderlayOffsetY(ui_TvTitleSpeed);
        }

        if (ui_TvTitleNumber)
        {
            ui_TvTitleNumber.outlineColor = Color.white;
            ui_TvTitleNumber.text = TextHandler.Instance.manager.GetTextById(3);
            SetUnderlayOffsetY(ui_TvTitleNumber);

        }     
        RefreshUI();
    }

    protected void SetUnderlayOffsetY(TextMeshProUGUI textMeshPro)
    {
        Material fontMaterial= textMeshPro.fontMaterial;
        fontMaterial.SetFloat("_UnderlayOffsetY", -0.8f);
    }

    public void RefreshUI()
    {

    }

    public void SetTextForAttribute(Text tvLevel, TextMeshProUGUI tvLevelMoney, int level, int maxLevel, long levelUpMoney)
    {
        if (tvLevel)
        {
            if (level == maxLevel)
            {
                tvLevel.text = "Lv.Max";
            }
            else
            {
                tvLevel.text = "Lv." + level;
            }
        }
        if (tvLevelMoney)
        {
            tvLevelMoney.text = levelUpMoney + "";
            tvLevelMoney.outlineColor = color_MoneyOutline;
        }
         
    }

    public void SetButtonStatusForAttribute(Button btAttribute, long levelUpMoney)
    {

    }

    public void OnClickForAddNumber()
    {

    }

    public void OnClickForAddGoldPrice()
    {
    
    }

    public void OnClickForAddSpeed()
    {

    }

}