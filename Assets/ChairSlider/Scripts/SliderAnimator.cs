using UnityEngine;
using DG.Tweening;

public class SliderAnimator : MonoBehaviour
{
    Transform[] _chairs;
    Vector3[] _chairsPositions;
    float[] _scales;

    [SerializeField]
    Ease _easeType = Ease.Linear;

    float _delay = 0.1f;

    void Start()
    {
        ChairSliderEvents.OnSlide += SlideChairs;

        _chairs = new Transform[transform.childCount];
        _scales = new float[transform.childCount];

        for (int i = 0; i < _chairs.Length; i++)
        {
            _chairs[i] = transform.GetChild(i);
            _scales[i] = transform.GetChild(i).localScale.x;
        }

    }

    Transform [] GetChildTranforms()
    {
        Transform[] _transforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _transforms[i] = transform.GetChild(i);
        }
        return _transforms;
    }

    void SlideChairs(Vector2 _direction, float _itemAnimationDuration)
    {

        Transform[] _chairs = GetChildTranforms();

        float _interval = _delay;

        if (_direction == Vector2.right)
        {


            for (int i = _chairs.Length - 1; i >= 0; i--) 
            {
                _interval += _delay;

                if (i + 1 < _chairs.Length)
                {
                    _chairs[i].DOMove(_chairs[i + 1].position, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                    _chairs[i].DOScale(_chairs[i + 1].localScale, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                }

                else
                {
                    _chairs[i].SetSiblingIndex(0);
                    _chairs[i].DOMove(_chairs[0].position, _itemAnimationDuration);
                }

            }
        }

        else
        {
            for (int i = 0; i < _chairs.Length; i++)
            {
                _interval += _delay;

                if (i > 0)
                {
                    _chairs[i].DOMove(_chairs[i - 1].position, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                    _chairs[i].DOScale(_chairs[i - 1].localScale, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                }

                else
                {
                    _chairs[i].SetSiblingIndex(_chairs.Length - 1);
                    _chairs[i].DOMove(_chairs[_chairs.Length - 1].position, _itemAnimationDuration);
                }

            }
        }

        ChairSliderEvents.ForceSwitchArrows();
        ChairSliderEvents.OpenUI(AnimationDurations.current.itemAnimationDuration, false);
        ChairSliderEvents.CloseUI(0, AnimationDurations.current.itemAnimationDuration, false, false);

    }

    private void OnDestroy()
    {
        ChairSliderEvents.OnSlide -= SlideChairs;
    }

}