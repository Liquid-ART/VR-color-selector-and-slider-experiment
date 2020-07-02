using System;
using UnityEngine;

public static class ChairSliderEvents
{
    public static event Action<int> OnStartControl;
    public static void StartControl(int _activeOptionIndex)
    {
        OnStartControl?.Invoke(_activeOptionIndex);
    }

    public static event Action<float, bool> OnOpenUI;
    public static void OpenUI(float _pathAnimationDuration, bool _openCounter)
    {
        OnOpenUI?.Invoke(_pathAnimationDuration, _openCounter);
    }

    public static event Action<int, float, bool, bool> OnCloseUI;
    public static void CloseUI(int _activeOptionIndex, float _pathAnimationDuration, bool _expandControl, bool _leaveHover)
    {
        OnCloseUI?.Invoke(_activeOptionIndex, _pathAnimationDuration, _expandControl, _leaveHover);
    }

    public static event Action<int, float, bool> OnChangeControl;
    public static void ChangeControl(int _activeOptionIndex, float _itemAnimDuration, bool _expand)
    {
        OnChangeControl?.Invoke(_activeOptionIndex, _itemAnimDuration, _expand);
    }


    public static event Action<Vector2, float> OnSlide;
    public static void Slide(Vector2 _direction, float _itemAnimDuration)
    {
        OnSlide?.Invoke(_direction, _itemAnimDuration);
    }

    public static event Action<Material, string, bool, int> OnChangeColor;
    public static void ChangeColor(Material _material, string _colorName, bool _isStartingControl, int _startSublingIndex)
    {

        OnChangeColor?.Invoke(_material, _colorName, _isStartingControl, _startSublingIndex);
    }

    public static event Action OnForceSwitchArrows;
    public static void ForceSwitchArrows()
    {
        OnForceSwitchArrows?.Invoke();
    }

    public static event Action<float> OnOpenCancelButton;
    public static void OpenCancelButton(float _itemAnimDuration)
    {
        OnOpenCancelButton?.Invoke(_itemAnimDuration);
    }

    public static event Action<float> OnCloseCancelButton;
    public static void CloseCancelButton(float _itemAnimDuration)
    {
        OnCloseCancelButton?.Invoke(_itemAnimDuration);
    }

    public static event Action<bool> OnEnableOrDisableArrows;
    public static void EnableOrDisableArrows(bool _enable)
    {
        OnEnableOrDisableArrows?.Invoke(_enable);
    }

    public static event Action OnPlayHoverSound;
    public static void PlayHoverSound()
    {
        OnPlayHoverSound?.Invoke();
    }

    public static event Action OnPlaPressSound;
    public static void PlayPressSound()
    {
        OnPlaPressSound?.Invoke();
    }


    public static event Action OnOpenCounter;
    public static void OpenCounter()
    {
        OnOpenCounter?.Invoke();
    }

    public static event Action OnCloseCounter;
    public static void CloseCounter()
    {
        OnCloseCounter?.Invoke();
    }
}
