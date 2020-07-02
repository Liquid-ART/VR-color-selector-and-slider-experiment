using UnityEngine;
using DG.Tweening;

struct TweeningObj
{
    public Transform transform;
    public float defaultScale;
    public Vector3 defaultRotation;
    public float notSelectedScale;
    float _expandedScale;

    public float ExpandedScale
    {
        get
        {
            _expandedScale = defaultScale / 4;
            return _expandedScale;
        }
 
    }
}


public class OptionsAnimator : MonoBehaviour
{

    [SerializeField]
    Transform _targetToLook, _optionUI;

    private Transform _pathHolder;
    Vector3[] _pathNotSelected, _pathSelected;
    PathType _pathType = PathType.CatmullRom;

    [SerializeField]
    Ease _easeType = Ease.Linear;

    TweeningObj _tweeningObj = new TweeningObj();


    private void Awake()
    {
        ChairSliderEvents.OnChangeControl += StartMove;
        ChairSliderEvents.OnStartControl += SetStartPosition;

        _pathHolder = transform.GetChild(1);
        _tweeningObj.transform = transform.GetChild(0);
        _tweeningObj.defaultScale = _tweeningObj.transform.localScale.x;
        _tweeningObj.defaultRotation = _tweeningObj.transform.rotation.eulerAngles;

        _pathNotSelected = GetPath(_pathHolder, true);
        _pathSelected = GetPath(_pathHolder, false);

        _optionUI = _pathHolder.GetChild(_pathHolder.childCount - 1).GetChild(0);
    }


    private void OnDrawGizmosSelected()
    {
        _pathHolder = transform.GetChild(1);
        Vector3 _startPosition = _pathHolder.GetChild(0).position;
        Vector3 _previousPosition = _startPosition;
        foreach (Transform _waypoint in _pathHolder)
        {
            Gizmos.DrawSphere(_waypoint.position, .1f);
            Gizmos.DrawLine(_previousPosition, _waypoint.position);
            _previousPosition = _waypoint.position;
        }
        Gizmos.DrawLine(_previousPosition, _startPosition);

        Gizmos.color = Color.red;
    }

    void SetStartPosition(int activeOptionIndex)
    {

        _pathNotSelected = GetPath(_pathHolder, true);
        _pathSelected = GetPath(_pathHolder, false);

        _optionUI.localScale = Vector3.zero;
        

        if (transform.GetSiblingIndex() != activeOptionIndex)
        {
            _tweeningObj.transform.position = _pathNotSelected[0];
            _tweeningObj.transform.localScale = new Vector3(_tweeningObj.notSelectedScale, _tweeningObj.notSelectedScale, _tweeningObj.notSelectedScale);
        }
    }

    Vector3[] GetPath(Transform _pathHolder, bool _secondary)
    {
        Vector3[] _path = new Vector3[_pathHolder.childCount];

        for (int i = 0; i < _path.Length; i++)
        {
            if (_secondary && i == 0)
            {
                _path[i] = _pathHolder.GetChild(i).GetChild(0).position;
                continue;
            }

            _path[i] = _pathHolder.GetChild(i).position;
        }

        return _path;
    }

    void StartMove(int activeObjIndex, float _duration, bool _isExpanding)
    {
        _pathNotSelected = GetPath(_pathHolder, true);
        _pathSelected = GetPath(_pathHolder, false);

        Vector3[] _activePath;
        float _endScale;

        if (transform.GetSiblingIndex() == activeObjIndex)
        {

            if (_isExpanding)
            {
                _activePath = _pathSelected;
                _endScale = _tweeningObj.ExpandedScale;
            }
            else
            {
                _activePath = FlipPath(_pathSelected);
                _endScale = _tweeningObj.defaultScale;
            }
        }

        else
        {
            if (_isExpanding)
            {
                _activePath = _pathNotSelected;
                _endScale = _tweeningObj.ExpandedScale;
            }
            else
            {
                _activePath = FlipPath(_pathNotSelected);
                _endScale = _tweeningObj.notSelectedScale;
            }

        }

        if (transform.parent.parent.GetSiblingIndex() == transform.parent.parent.parent.childCount /2)//need refactoring
        {
            FollowPath(_activePath, _endScale, _targetToLook, _isExpanding, _duration);
        }

    }


    void FollowPath(Vector3[] _path, float _endScale, Transform _lookTarget, bool _isExpanding, float _duration)
    {
        if (_isExpanding)
        {
            _tweeningObj.transform.DOPath(_path, _duration, _pathType).SetEase(_easeType).SetLookAt(_lookTarget)
                .OnComplete(() =>  {
                _optionUI.DOScale(1, _duration / 3)
                .OnComplete(() => {
                    if (transform.GetSiblingIndex() == 0)
                    ChairSliderEvents.OpenCancelButton(ChairSliderInfo.current.itemAnimationDuration);});
                });

            _tweeningObj.transform.DOScale(_endScale, _duration).SetEase(_easeType);
        }

        else
        {
            _optionUI.DOScale(0, _duration / 3);
            _tweeningObj.transform.DOPath(_path, _duration, _pathType).SetEase(_easeType).SetDelay(_duration / 3);
            _tweeningObj.transform.DOScale(_endScale, _duration).SetEase(_easeType).SetDelay(_duration / 3);
            _tweeningObj.transform.DORotate(_tweeningObj.defaultRotation, _duration).SetEase(_easeType).SetDelay(_duration / 3);
        }

    }


    Vector3[] FlipPath(Vector3[] _array)
    {

        Vector3[] _newArray = new Vector3[_array.Length];

        for (int i = 0; i < _array.Length; i++)
        {
           _newArray[i] = _array[(_array.Length - 1) - i];
        }

        return _newArray;
    }


    private void OnDestroy()
    {
        ChairSliderEvents.OnChangeControl -= StartMove;
        ChairSliderEvents.OnStartControl -= SetStartPosition;
    }
}
