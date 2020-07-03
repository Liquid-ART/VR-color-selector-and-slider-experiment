using UnityEngine;
using DG.Tweening;

public class CounterAnimator : MonoBehaviour
{
    [SerializeField]
    private Ease _easeType = Ease.Linear;
    [SerializeField]
    private float _delay = 0.1f;

    private Color _selectedColor, _notSelectedColor;


    void Start()
    {
        ChairSliderEvents.OnSlide += SlideItems;
        ChairSliderEvents.OnCloseCounter += HideCounter;
        ChairSliderEvents.OnOpenCounter += ShowCounter;
        _notSelectedColor = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        _selectedColor = transform.GetChild(transform.childCount / 2).gameObject.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");

    }


    private Transform[] GetChildTranforms()
    {
        Transform[] _transforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _transforms[i] = transform.GetChild(i);
        }
        return _transforms;
    }

    private void SlideItems(Vector2 _direction, float _itemAnimationDuration)
    {
        Material[] _materials = new Material[transform.childCount]; ;

        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i] = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material;
        }

        Transform[] _items = GetChildTranforms();


        float _interval = _delay;

        if (_direction == Vector2.right)
        {


            for (int i = _items.Length - 1; i >= 0; i--) 
            {
                _interval += _delay;

                if (i + 1 < _items.Length)
                {
                    _items[i].DOMove(_items[i + 1].position, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                    _items[i].DOScale(_items[i + 1].localScale, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                }

                else
                {

                    _items[i].SetSiblingIndex(0);
                    _items[i].DOMove(_items[0].position, _itemAnimationDuration);
                }

                ChangeColor(_items, _materials, _itemAnimationDuration, _interval, i);
            }
        }

        else
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _interval += _delay;

                if (i > 0)
                {
                    _items[i].DOMove(_items[i - 1].position, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                    _items[i].DOScale(_items[i - 1].localScale, _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
                }

                else
                {
                    _items[i].SetSiblingIndex(_items.Length - 1);
                    _items[i].DOMove(_items[_items.Length - 1].position, _itemAnimationDuration);
                }

                ChangeColor(_items, _materials, _itemAnimationDuration, _interval, i);
            }
        }

    }

    private void ChangeColor(Transform[] _items, Material[] _materials, float _itemAnimationDuration, float _interval, int _i)
    {
        if (_items[_i].GetSiblingIndex() == transform.childCount / 2)
        {
            _materials[_i].DOColor(_selectedColor, "_BaseColor", _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
        }
        else
        {
            _materials[_i].DOColor(_notSelectedColor, "_BaseColor", _itemAnimationDuration).SetEase(_easeType).SetDelay(_interval);
        }
    }

    private void HideCounter()
    {
            transform.DOScale(0, AnimationDurations.current.itemAnimationDuration/2).SetEase(_easeType);
    }

    private void ShowCounter()
    {
            transform.DOScale(1, AnimationDurations.current.itemAnimationDuration / 2).SetEase(_easeType)
                .SetDelay(AnimationDurations.current.pathAnimationDuration + AnimationDurations.current.itemAnimationDuration);
    }

    private void HideCounterOnHoverExit(int _activeOptionIndex, float _itemAnimDuration, bool _expand)
    {
        if (_expand)
            transform.DOScale(0, _itemAnimDuration / 2).SetEase(_easeType);
    }

    private void ShowCounterOnHoverEnter(int _activeOptionIndex, float _itemAnimDuration, bool _expand)
    {
        if (!_expand)
            transform.DOScale(1, _itemAnimDuration / 2).SetEase(_easeType)
                .SetDelay(AnimationDurations.current.pathAnimationDuration + AnimationDurations.current.itemAnimationDuration);
    }

    private void OnDestroy()
    {
        ChairSliderEvents.OnSlide -= SlideItems;
        ChairSliderEvents.OnCloseCounter -= HideCounter;
        ChairSliderEvents.OnOpenCounter -= ShowCounter;
    }


}
