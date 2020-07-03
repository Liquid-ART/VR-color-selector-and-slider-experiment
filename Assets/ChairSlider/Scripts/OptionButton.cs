using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionButton : MonoBehaviour
{

    [SerializeField]
    private int activeOptionIndex;

    private Button _button;
    private Material _material;
    private string _colorName;
    private int _startSublingIndex;
    private Timer _timer = new Timer();

    private AnimationDurations _chairSliderInfo;

    void Awake()
    {
        _chairSliderInfo = GetComponentInParent<AnimationDurations>();
        _colorName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        _colorName = _colorName.Replace(" ", String.Empty);

        _material = transform.GetChild(1).GetComponent<MeshRenderer>().material;

        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);

        _startSublingIndex = gameObject.GetComponentInParent<ChairUIAnimator>().transform.GetSiblingIndex();


    }

    private void Start()
    {
        if (activeOptionIndex == gameObject.GetComponentInParent<ChairUIAnimator>().activeOptionIndex)
        {
            ChairSliderEvents.ChangeColor(_material, _colorName, true, _startSublingIndex);
        }
    }


    public void ButtonClick()
    {
        gameObject.GetComponentInParent<ChairUIAnimator>().activeOptionIndex = activeOptionIndex;

        ChairSliderEvents.CloseCancelButton(_chairSliderInfo.itemAnimationDuration);

        ChairSliderEvents.ChangeControl(activeOptionIndex, _chairSliderInfo.pathAnimationDuration, false);
        ChairSliderEvents.OpenUI(_chairSliderInfo.pathAnimationDuration, true);
        ChairSliderEvents.ChangeColor(_material, _colorName, false, _startSublingIndex);
    }


    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
