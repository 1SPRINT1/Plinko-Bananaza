using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TogglerElement : CheckerElement
{
    [SerializeField] protected TMP_Text _descriptionField;
    [SerializeField] protected Image _icon;
    [SerializeField] private Image _togglerImage;

    [Space] 
    [SerializeField] protected Sprite _iconOn;
    [SerializeField] protected Sprite _iconOff;
    
    [Space] 
    [SerializeField] private RectTransform _positionFirst;
    [SerializeField] private RectTransform _positionSecond;
    
    protected override void OnChangeButtonInteractivity(bool state)
    {
        string value = state ? "On" : "Off";
        _icon.sprite = state ? _iconOn : _iconOff;
        _descriptionField.text = $"{value}";

        _togglerImage.transform.position = state ? _positionSecond.position : _positionFirst.position;
        _descriptionField.transform.position = state ? _positionFirst.position : _positionSecond.position;
    }
}