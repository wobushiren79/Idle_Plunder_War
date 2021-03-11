using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIViewForAttributeUp : BaseMonoBehaviour
{

    public Button ui_BtAdd;

    public Text ui_TvLevel;

    public Text ui_TvMoney;

    protected ICallBack callBack;

    public void SetData(int level,long gold)
    {
        ui_TvLevel.text = "Lv." + level;
        ui_TvMoney.text = gold + "";
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