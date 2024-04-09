using TMPro;
using UnityEngine;

public abstract class InputElement : MonoBehaviour, ILayoutElement
{
    [SerializeField] private TMP_InputField _field;

    private ILabelCommand _command;

    public void OnShowElement()
    {
    }

    public void OnHideElement()
    {
        if (_command != null)
            _command.Changed -= OnCommandStateChanged;

        if (_field != null && _command != null)
            _field.onValueChanged.RemoveAllListeners();
    }

    public void OnChange(ILabelCommand command)
    {
        _command = command;

        if (_command != null)
            _command.Changed += OnCommandStateChanged;

        if (_field != null && _command != null)
            _field.onValueChanged.AddListener(value => _command.State = value);
    }

    private void OnCommandStateChanged(string value)
    {
        _field.text = value;
    }
}