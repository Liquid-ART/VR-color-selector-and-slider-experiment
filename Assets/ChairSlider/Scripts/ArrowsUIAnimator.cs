using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ArrowsUIAnimator : MonoBehaviour
{
    [SerializeField]
    private Ease _uiEaseType;

    [SerializeField]
    private RectTransform _arrowRight, _arrowLeft;

    [SerializeField]
    private float _arrowStartScale = 120f;

    [SerializeField]
    private Image _imageOutline;

    private Sequence _arrowSequence;
    private Timer _expandArrowTimer = new Timer();
    private float _tweenImageOutline = 1;

    void Start()
    {
        ChairSliderEvents.OnSlide += HideArrowInActiveSelector;
        ChairSliderEvents.OnForceSwitchArrows += SwitchArrows;
        ChairSliderEvents.OnEnableOrDisableArrows += EnableOrDisableArrows;
        SwitchArrows();
    }

    private void Update()
    {
        //Update outline opacity
        _imageOutline.color = new Color(255, 255, 255, _tweenImageOutline);

        if (_expandArrowTimer.OnceTimerIsComplete())
        {
            DOTween.To(() => _tweenImageOutline, x => _tweenImageOutline = x, 1, AnimationDurations.current.itemAnimationDuration / 2).SetEase(_uiEaseType);
        }

    }

    private void SwitchArrows()
    {
        if (transform.GetSiblingIndex() > transform.parent.childCount / 2)//right
        {
            SwitchArrowsTweens(_arrowLeft, _arrowRight);
        }

        else if (transform.GetSiblingIndex() < transform.parent.childCount / 2)//left
        {
            SwitchArrowsTweens(_arrowRight, _arrowLeft);
        }

        else if (transform.GetSiblingIndex() == transform.parent.childCount / 2)//center
        {
            ShowOrHideArrowTweens(_arrowRight, AnimationDurations.current.itemAnimationDuration, false);
            ShowOrHideArrowTweens(_arrowLeft, AnimationDurations.current.itemAnimationDuration, false);
        }
    }


    private void EnableOrDisableArrows(bool _enableArrow) //show or hide arrows if selector is open
    {
        Transform arrow;
        if (_arrowRight.gameObject.activeSelf)
            arrow = _arrowRight;        
        else
            arrow = _arrowLeft;

        if (transform.GetSiblingIndex() > transform.parent.childCount / 2)//right
        {
            ShowOrHideArrowTweens(arrow, AnimationDurations.current.itemAnimationDuration, _enableArrow);
        }

        else if (transform.GetSiblingIndex() < transform.parent.childCount / 2)//left
        {
            ShowOrHideArrowTweens(arrow, AnimationDurations.current.itemAnimationDuration, _enableArrow);
        }
    }


    private void SwitchArrowsTweens(Transform from, Transform to) //tweens to SwitchArrows()
    {
        from.localScale = Vector3.zero;
        from.gameObject.SetActive(false);

        to.gameObject.SetActive(true);
        to.DOScale(_arrowStartScale, AnimationDurations.current.itemAnimationDuration / 2).SetEase(_uiEaseType).SetDelay(AnimationDurations.current.itemAnimationDuration * 2);
        _expandArrowTimer.SetTimer(AnimationDurations.current.itemAnimationDuration * 2);
    }

    private void HideArrowInActiveSelector(Vector2 direction, float _itemAnimationDuration) //hide arrow if selector is active
    {

        if (transform.GetSiblingIndex() == transform.parent.childCount / 2)
        {
            if (_arrowRight.gameObject.activeSelf)
            {
                ShowOrHideArrowTweens(_arrowRight, _itemAnimationDuration, false);
            }
            else
            {
                ShowOrHideArrowTweens(_arrowLeft, _itemAnimationDuration, false);
            }
        }
    }

    private void ShowOrHideArrowTweens(Transform arrow, float _itemAnimationDuration, bool _enableArrow)//tweens for HideArrowInActiveSelector()
    {
        float _endScale = 0;
        float _endOpacity = 0;
        if(_enableArrow)
        {
            _endScale = _arrowStartScale;
            _endOpacity = 1;
        }

        arrow.DOScale(_endScale, _itemAnimationDuration / 2).SetEase(_uiEaseType);
        DOTween.To(() => _tweenImageOutline, x => _tweenImageOutline = x, _endOpacity, _itemAnimationDuration / 2).SetEase(_uiEaseType);
    }

    private void OnDestroy()
    {
        ChairSliderEvents.OnSlide -= HideArrowInActiveSelector;
        ChairSliderEvents.OnForceSwitchArrows -= SwitchArrows;
        ChairSliderEvents.OnEnableOrDisableArrows -= EnableOrDisableArrows;

    }


}
