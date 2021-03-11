using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIViewForAttributeUp : BaseMonoBehaviour
{
    public Image ui_IvBackground;

    public Button ui_BtAdd;

    public Text ui_TvLevel;

    public Text ui_TvMoney;

    public Sprite spBackUnClick;
    public Sprite spBackClick;

    protected ICallBack callBack;

    private void Awake()
    {
        if (ui_BtAdd)
            ui_BtAdd.onClick.AddListener(OnClickForAdd);
    }

    public void OnClickForAdd()
    {
        if (callBack != null)
            callBack.OnClickForAttributeAdd(this);
    }

    public void SetData(int level,long gold,bool isClick)
    {
        ui_TvLevel.text = "Lv." + level;
        ui_TvMoney.text = gold + "";
        if (isClick)
        {
            ui_IvBackground.sprite = spBackClick;
        }
        else
        {
            ui_IvBackground.sprite = spBackUnClick;
        }
    }

    public void SetCallBack(ICallBack callBack)
    {
        this.callBack = callBack;
    }

    public interface ICallBack
    {
        void OnClickForAttributeAdd(UIViewForAttributeUp view);
    }
}