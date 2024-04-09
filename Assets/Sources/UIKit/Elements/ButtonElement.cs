using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonElement : MonoBehaviour, ILayoutElement
{
    [SerializeField, HideInInspector] private Button _button;

    protected IButtonCommand _command;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
        name = $"[{GetType().Name}]";
    }

    public void OnShowElement()
    {
    }

    public void OnHideElement()
    {
        if (_command != null)
            _command.Changed -= OnButtonStateChanged;

        if (_button != null && _command != null) {
            _button.onClick.RemoveAllListeners();
            _command = null;
        }
    }

    public void OnClick(IButtonCommand command)
    {
        _command = command;

        if (_command != null)
            _command.Changed += OnButtonStateChanged;

        if (_button != null && _command != null) {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => {
                _command.Execute();
                OnButtonClick();
            });
        }
            
    }
    protected virtual void OnButtonClick() {}

    private void OnButtonStateChanged(bool state)
    {
        _button.interactable = OnChangeButtonInteractivity(state);
        
    }

    protected virtual bool OnChangeButtonInteractivity(bool state) {
        return state;
    }

    public virtual void Highlight(bool play) {}

}