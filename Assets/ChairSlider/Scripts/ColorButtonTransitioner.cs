using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ColorButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color _sphereColor, _defaultColor = Color.white;
    [SerializeField]
    private Transform _sphere;
    private TextMeshProUGUI _tmPro;

    private Transform _parentSelector;


    void Start()
    {
        ChairSliderEvents.OnChangeColor += ChangeColor;
        _parentSelector = GetComponentInParent<ChairUIAnimator>().gameObject.transform;
        _sphereColor = _sphere.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        _tmPro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _tmPro.color = _sphereColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tmPro.color = _defaultColor;
    }

    private void ChangeColor(Material _material, string _notUsing, bool _isStartingControl, int _notUsing3)
    {
        if (_parentSelector.GetSiblingIndex() == _parentSelector.parent.childCount / 2 || _isStartingControl) 
            _sphereColor = _material.GetColor("_BaseColor");
    }

    private void OnDestroy()
    {

        ChairSliderEvents.OnChangeColor -= ChangeColor;
    }    
}
