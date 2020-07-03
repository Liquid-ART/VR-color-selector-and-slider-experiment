using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    [SerializeField]
    private bool _isRight;

    private Button _button;

    void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(Click);
    }

    void Click()
    {
        if (_isRight)
            ChairSliderEvents.Slide(Vector2.left, AnimationDurations.current.itemAnimationDuration);
        else 
            ChairSliderEvents.Slide(Vector2.right, AnimationDurations.current.itemAnimationDuration);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
