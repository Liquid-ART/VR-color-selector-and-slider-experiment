using UnityEngine;

public class ChairSliderInfo : MonoBehaviour
{
    [Range(0f, 1f)]
    public float pathAnimationDuration = 0.7f;
    [Range(0f, 1f)]
    public float itemAnimationDuration = 0.3f;

    public static ChairSliderInfo current;

    [SerializeField]
    private BoxCollider _uiCollider;

    [SerializeField]
    private Transform _pointer;

    private bool _isControlExpanded, _isClosingControl;

    private GameObject _lastHittedObject;
    private Timer _timerHideArrowsOnStart = new Timer();

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {

        _timerHideArrowsOnStart.SetTimer(2);

        ChairSliderEvents.OnChangeControl += ChangeBool;
        ChairSliderEvents.StartControl(1);
        ChairSliderEvents.CloseUI(1, pathAnimationDuration, false, false);
    }

    private void Update()
    {

        if(_timerHideArrowsOnStart.OnceTimerIsComplete())
        {
            ChairSliderEvents.EnableOrDisableArrows(false);
            ChairSliderEvents.CloseCounter();
        }


        RaycastHit _hit = CreateRaycast(_pointer);

        if (_hit.collider == _uiCollider)
        {
            if (IsEnterHovering())
            {
                print(_lastHittedObject != _uiCollider);
                print(_uiCollider);
                ChairSliderEvents.OpenUI(ChairSliderInfo.current.itemAnimationDuration, true);
                ChairSliderEvents.OpenCounter();
            }

        }
        else if (IsExitHovering())
        {
            ChairSliderEvents.CloseUI(0, ChairSliderInfo.current.itemAnimationDuration, false, true);
            ChairSliderEvents.CloseCounter();
        }


        if (_hit.collider != null)
            _lastHittedObject = _hit.collider.gameObject;
        else
            _lastHittedObject = null;
    }

    private bool IsEnterHovering()
    {
        bool _isEnterHovering = false;

        if (_lastHittedObject != _uiCollider.gameObject && !_isControlExpanded)
        {
            _isEnterHovering = true;
        }
        return _isEnterHovering;
    }

    private bool IsExitHovering()
    {
        bool _isExitHovering = false;

        if (_lastHittedObject == _uiCollider.gameObject && !_isControlExpanded)
        {
            _isExitHovering = true;
        }
        return _isExitHovering;
    }


    private void ChangeBool(int _notUsed, float _notUsed2, bool isExpanding)
    {
        if (isExpanding)
            _isControlExpanded = true;
        else
        {
            _isControlExpanded = false;
        }

    }

    private RaycastHit CreateRaycast(Transform _pointer)
    {
        RaycastHit hit;
        Ray ray = new Ray(_pointer.position, _pointer.forward);
        Physics.Raycast(ray, out hit);
        return hit;
    }
}
