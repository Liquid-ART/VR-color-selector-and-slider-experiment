using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundEvents : MonoBehaviour, IPointerEnterHandler
{

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChairSliderEvents.PlayHoverSound();
    }

    private void PlayClickSound()
    {
        ChairSliderEvents.PlayPressSound();
    }
    










}
