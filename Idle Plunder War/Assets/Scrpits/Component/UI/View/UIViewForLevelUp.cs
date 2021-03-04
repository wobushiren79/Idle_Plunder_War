using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIViewForLevelUp : BaseUIView
{
    public Button ui_BtLevelUp;
    public ProgressView ui_PvLevelUp;
    public ICallBack callBack;

    protected Tween animForLevelUp; 
    public override void Awake()
    {
        base.Awake();
        if (ui_PvLevelUp)
        {
            ui_PvLevelUp.SetCompleteContent(TextHandler.Instance.manager.GetTextById(4));
        }
         
        if (ui_BtLevelUp)
            ui_BtLevelUp.onClick.AddListener(OnClickForLevelUp);
    }

    public void SetCallBack(ICallBack callBack)
    {
        this.callBack = callBack;
    }

    public void SetLevelUpPro(int level, float pro)
    {
        ui_PvLevelUp.SetData("Lv." + level, pro);
        AnimForWaitLevelUp(pro);
    }

    public void OnClickForLevelUp()
    {
        if (callBack != null)
            callBack.OnClickForLevelUp();
    }

    public void AnimForWaitLevelUp(float pro)
    {
        if (pro < 1)
        {
            animForLevelUp = null;
            transform.DOKill();
            transform.localScale = Vector3.one;
        }
        else
        {
            if (animForLevelUp == null)
            {
                animForLevelUp = transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
            }
        }
    }

    public interface ICallBack
    {
        void OnClickForLevelUp();
    }
}