using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ChairUIAnimator : MonoBehaviour
{
    [SerializeField]
    private Transform _sphere, _chairUI;
    [SerializeField]
    private RectTransform _buttonBuy;

    private Sequence _uiSequence;

    [HideInInspector]
    public int activeOptionIndex = 1;

    [SerializeField]
    private Image uiImageBG;

    private float _tweenImageBG = 1;

    [SerializeField]
    private Ease uiEaseType = Ease.Linear;

    private Timer _openUiTimer = new Timer();
    private Timer _playingUiTimer = new Timer();
    private bool _isClosed;

 

    //color button on UI
    [SerializeField]
    private Button _colorButton;
    [SerializeField]
    private GridLayoutGroup _grid;

    private TextMeshProUGUI _colorName;

    private Material _material;

    [SerializeField]
    private float _paddingSum = 32;

    //scales for uiSequence
    private float _buttonBuyScaleStart;
    private float _chairUIScaleStart;
    private float _sphereScaleStart; 

    private void Awake()
    {

        ChairSliderEvents.OnOpenUI += OpenUI;
        ChairSliderEvents.OnCloseUI += CloseUI;
        ChairSliderEvents.OnChangeColor += ChangeColorInUI;

        _colorButton.onClick.AddListener(() => { ChairSliderEvents.CloseUI(activeOptionIndex, ChairSliderInfo.current.pathAnimationDuration, true, false); });
        _colorName = _colorButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _buttonBuyScaleStart = _buttonBuy.localScale.x;
        _chairUIScaleStart = _chairUI.localScale.x;
        _sphereScaleStart = _sphere.localScale.x;
    }

    void Update()
    {
        uiImageBG.color = new Color(1, 1, 1, _tweenImageBG);

        if (_openUiTimer.OnceTimerIsComplete())
        {
           _uiSequence.Play();
            ChairSliderEvents.EnableOrDisableArrows(true);

        }
    }

    private void ChangeColorInUI(Material _material, string _colorName, bool _isStartingControl, int _startSublingIndex)
    {

        if (!_isStartingControl && transform.GetSiblingIndex() == transform.parent.childCount / 2 
           || _isStartingControl && _startSublingIndex == transform.GetSiblingIndex())
        {
            _sphere.GetComponent<MeshRenderer>().material = _material;
            this._colorName.text = _colorName;
            Vector3 width = this._colorName.GetPreferredValues();
            _grid.cellSize = new Vector2(width.x + _paddingSum, 100);
        }
 
    }

    private void CloseUI(int _activeOptionIndex, float _pathAnimationDuraion, bool _expandConrol, bool _leaveHover)
    {
        if (!_isClosed)
        {
            _playingUiTimer.SetTimer(_pathAnimationDuraion);
            if (!_expandConrol && transform.GetSiblingIndex() != transform.parent.childCount / 2 ||
                _expandConrol && transform.GetSiblingIndex() == transform.parent.childCount / 2 ||
                _leaveHover && transform.GetSiblingIndex() == transform.parent.childCount / 2)
            {
                ChairSliderEvents.EnableOrDisableArrows(false);
                UiSequence(_activeOptionIndex, _pathAnimationDuraion, _expandConrol, false);
                _isClosed = true;
            }

            if (_expandConrol || _leaveHover)
                ChairSliderEvents.CloseCounter();
        }

    }

    private void OpenUI(float _pathAnimationDuration, bool _openCounter)
    {

        if (transform.GetSiblingIndex() == transform.parent.childCount / 2 && _isClosed)
        {
            _playingUiTimer.SetTimer(_pathAnimationDuration);
            UiSequence(0, _pathAnimationDuration, false, true);
            _openUiTimer.SetTimer(_pathAnimationDuration);
            _isClosed = false;

            if (_openCounter)
                ChairSliderEvents.OpenCounter();
        }

    }


    private void UiSequence(int _activeOptionIndex, float _pathAnimationDuraion, bool _expandConrol, bool _playingBackwards)
    {

        float _buttonBuyEndScale = 0;
        float _sphereEndScale = 0;
        float _chairUIEndScale = 0;
        float _tweenImageBGEndOpacity = 0;

        if (_playingBackwards)
        {
            _buttonBuyEndScale = _buttonBuyScaleStart;
            _sphereEndScale = _sphereScaleStart;
            _chairUIEndScale = _chairUIScaleStart;
            _tweenImageBGEndOpacity = 1;
        }

        _uiSequence = DOTween.Sequence().SetAutoKill(false);
        _uiSequence
            .Append(_buttonBuy.DOScale(_buttonBuyEndScale, ChairSliderInfo.current.itemAnimationDuration / 2).SetEase(uiEaseType))
            .Append(_sphere.DOScale(_sphereEndScale, ChairSliderInfo.current.itemAnimationDuration / 2).SetEase(uiEaseType))
            .Append(_chairUI.DOScale(_chairUIEndScale, ChairSliderInfo.current.itemAnimationDuration).SetEase(uiEaseType))
            .Insert(ChairSliderInfo.current.itemAnimationDuration, DOTween.To(() => _tweenImageBG, x => _tweenImageBG = x, _tweenImageBGEndOpacity, ChairSliderInfo.current.itemAnimationDuration / 2).SetEase(uiEaseType))
            .AppendCallback(() => { ExpandControlOptions(_activeOptionIndex, _pathAnimationDuraion, _expandConrol); });

        if (_playingBackwards)
        {
            _uiSequence.Pause();
        }
    }




    private void ExpandControlOptions(int _activeOptionIndex, float _pathAnimationDuraion, bool _expandControl)
    {
        if (!_uiSequence.IsBackwards() && _expandControl)
        {
            ChairSliderEvents.ChangeControl(_activeOptionIndex, _pathAnimationDuraion, true);
        }

    }


    private void OnDestroy()
    {
        ChairSliderEvents.OnOpenUI -= OpenUI;
        ChairSliderEvents.OnCloseUI -= CloseUI;
        ChairSliderEvents.OnChangeColor -= ChangeColorInUI;
    }

}
