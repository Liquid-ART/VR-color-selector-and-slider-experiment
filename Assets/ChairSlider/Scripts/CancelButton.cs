using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CancelButton : MonoBehaviour
{

    [SerializeField]
    private Ease _easeType = Ease.Linear;

    private Button _button;

    private float _startScale = 1;

    void Start()
    {
        ChairSliderEvents.OnOpenCancelButton += OpenButton;
        ChairSliderEvents.OnCloseCancelButton += CloseButton;
        CloseButton(0.1f);


        _button = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        int _activeOptionIndex = gameObject.GetComponentInParent<ChairUIAnimator>().activeOptionIndex;

        ChairSliderEvents.CloseCancelButton(ChairSliderInfo.current.itemAnimationDuration);
        ChairSliderEvents.OpenUI(ChairSliderInfo.current.pathAnimationDuration, true);
        ChairSliderEvents.ChangeControl(_activeOptionIndex, ChairSliderInfo.current.pathAnimationDuration, false);
    }

    private void OpenButton(float _itemAnimationDuration)
    {
        if(transform.parent.GetSiblingIndex() == transform.parent.parent.childCount /2)
            transform.DOScale(_startScale, _itemAnimationDuration / 2).SetEase(_easeType);
    }

    private void CloseButton(float _itemAnimationDuration)
    {
            transform.DOScale(0, _itemAnimationDuration / 2).SetEase(_easeType);
    }


}
