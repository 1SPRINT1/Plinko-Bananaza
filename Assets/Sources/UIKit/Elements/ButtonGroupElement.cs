using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISelectButton
{
    int Index { get; }
    bool State { get; }

    event Action<ISelectButton> Clicked;
    void SetSelectedState(bool state);

    void SetCheckedState(bool state);
}

public abstract class ButtonGroupElement : MonoBehaviour, ILayoutElement
{
    [SerializeField] private bool _deselectSelf = true;

    private readonly List<ISelectButton> _buttons = new();
    private IValueCommand<byte> _command;

    private ISelectButton _selected;

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

        _buttons.Each(b =>
        {
            b.Clicked -= OnButtonClicked;
            b.SetSelectedState(false);
            _buttons.Remove(b);
        });
    }

    public void OnSelect(IValueCommand<byte> command)
    {
        _command = command;

        GetButtonList();

        if (_command != null)
            _command.Changed += OnCommandStateChanged;
    }

    private void GetButtonList()
    {
        if (_buttons.Count == 0)
            GetComponentsInChildren<ISelectButton>(true)
                .Each(b =>
                {
                    b.Clicked += OnButtonClicked;
                    _buttons.Add(b);
                });
    }

    private void OnCommandStateChanged(byte index)
    {
        ISelectButton button = _buttons.FirstOrDefault(b => b.Index == index);

        if (button == null) return;

        _selected = button;
        _buttons.Each(b => b.SetSelectedState(b == button));
    }

    private void OnButtonClicked(ISelectButton button)
    {
        if (_selected == button)
        {
            if (_deselectSelf)
            {
                _selected.SetSelectedState(!_selected.State);
                _selected = null;
                _command.Execute(0);
            }

            return;
        }

        _selected = button;
        _buttons.Each(b => b.SetSelectedState(b == button));
        _command.Execute((byte)_selected.Index);
    }

    public void SetElementChecked(byte index)
    {
        GetButtonList();
        _buttons.Each(button => button.SetCheckedState(button.Index == index));
    }
    
    public void SetElementSelected(byte index)
    {
        GetButtonList();
        OnCommandStateChanged(index);
    }
}