using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIViewForButtonExtension : BaseMonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public enum ButtonType
    {
        None,
        Animated,
        TouchUpSideDownAnimated,
    }

    public float pressDurationTime = 1;
    public bool responseOnceByPress = false;
    public float doubleClickIntervalTime = 0.5f;

    public ButtonType mType = ButtonType.None;

    public UnityEvent onDoubleClick;
    public UnityEvent onPress;
    public UnityEvent onClick;
    public UnityEvent OnPressEnd;

    private bool isDown = false;
    private bool isPress = false;
    private float downTime = 0;

    private float clickIntervalTime = 0;
    private int clickTimes = 0;

    private Vector3 defaultScale = Vector3.one;

    private Button _btn;

    private void Awake()
    {
        defaultScale = transform.localScale;
        _btn = transform.GetComponent<Button>();
    }

    void Update()
    {
        if (isDown)
        {
            if (responseOnceByPress && isPress)
            {
                return;
            }
            downTime += Time.deltaTime;
            if (downTime > pressDurationTime)
            {
                isPress = true;
                onPress.Invoke();
            }
        }
        if (clickTimes >= 1)
        {
            clickIntervalTime += Time.deltaTime;
            if (clickIntervalTime >= doubleClickIntervalTime)
            {
                if (clickTimes >= 2)
                {
                    onDoubleClick.Invoke();
                }
                else
                {
                    onClick.Invoke();
                }
                clickTimes = 0;
                clickIntervalTime = 0;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        downTime = 0;
        DoAniamtion();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        if (isPress)
        {
            OnPressEnd?.Invoke();
        }
        DoAniamtion(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
        isPress = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isPress)
        {
            //onClick.Invoke();
            clickTimes += 1;
            OnButtonClickPlaySound();
        }
        else
            isPress = false;
    }


    private Tweener _ani;
    private void DoAniamtion(bool isEnter = true)
    {
        if (!(_btn?.IsInteractable() ?? false))
        {
            return;
        }
        _ani.Kill();
        switch (mType)
        {
            case ButtonType.TouchUpSideDownAnimated:
                if (isEnter)
                {
                    _ani = transform.DOScale(defaultScale * 0.9f, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                }
                else
                {
                    _ani = transform.DOScale(defaultScale, 0.5f).SetEase(Ease.OutElastic).SetUpdate(true);
                }
                break;
            case ButtonType.Animated:
                if (isEnter)
                {
                    _ani = transform.DOScale(defaultScale * 0.9f, 0.2f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                    {
                        _ani = transform.DOScale(defaultScale, 0.5f).SetEase(Ease.OutElastic).SetUpdate(true);
                    });
                }
                break;
        }
    }

    public static void GrayUIAndSubImages(Transform parent)
    {
        Material def = Resources.Load<Material>("Materials/GrayM");
        foreach (var item in parent.GetComponentsInChildren<Image>())
        {
            item.material = def;
        }
    }

    public static void ClearGrayUIAndSubImages(Transform parent)
    {
        foreach (var item in parent.GetComponentsInChildren<Image>())
        {
            item.material = null;
        }
    }

    public static void GrayButtonAndSubImages(Button btn)
    {
        btn.enabled = false;
        GrayUIAndSubImages(btn.transform);
    }

    public static void ClearButtonGary(Button btn)
    {
        btn.enabled = true;
        ClearGrayUIAndSubImages(btn.transform);
    }

    private void OnButtonClickPlaySound()
    {
        //SoundsManger.Instance.PlaySound(AudioSourceType.sound_btnclick);
    }

}