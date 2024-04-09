using TMPro;
using UnityEngine;

public abstract class LabelElement : MonoBehaviour, ILayoutElement
{
    [SerializeField] private TMP_Text _field;

    private ILabelCommand _command;

    private void OnValidate()
    {
        name = $"<{GetType().Name}>";
    }

    public void OnShowElement()
    {
    }

    public void OnHideElement()
    {
        if (_command != null)
            _command.Changed -= OnCommandStateChanged;
    }

    public void OnChange(ILabelCommand command)
    {
        _command = command;

        if (_command != null)
            _command.Changed += OnCommandStateChanged;
    }

    private void OnCommandStateChanged(string value)
    {
        _field.text = value;
    }
}