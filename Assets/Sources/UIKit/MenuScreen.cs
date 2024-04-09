using System;
using UnityEngine;
using Zenject;

public abstract class MenuScreen : MonoBehaviour
{
    [SerializeField] private LayoutAnimator _animator;
    
    [SerializeField,HideInInspector] private ScreenLayout _layout;

    private void OnValidate()
    {
        _layout = GetComponentInChildren<ScreenLayout>();
        name = $"{GetType().Name}";
    }

    [Inject]
    private void Construct()
    {
        gameObject.SetActive(false);
        _layout.Construct();
    }

    public void OnButtonClick<TButton>(IButtonCommand command) where TButton : ButtonElement => _layout.OnButtonClick<TButton>(command);

    protected void OnLabelShow<TLabel>(ILabelCommand command) where TLabel : LabelElement => _layout.OnLabelShow<TLabel>(command);

    protected void OnCheckerClick<TChecker>(IButtonCommand command) where TChecker : CheckerElement => _layout.OnCheckerClick<TChecker>(command);

    protected void OnSliderChange<TSlider>(IValueCommand<float> command) where TSlider : SliderElement => _layout.OnSliderChange<TSlider>(command);

    protected void OnSelectFromGroup<TGroup>(IGroupBoxCommand command) where TGroup : ButtonGroupElement => _layout.OnSelectFromGroup<TGroup>(command);

    protected void SetSelectedFromGroup<TGroup>(byte index) where TGroup : ButtonGroupElement => _layout.SetSelectedFromGroup<TGroup>(index);

    public void Show(Action complete)
    {
        _animator.Show(() =>
        {
            _layout.OnShowLayout();

            OnScreenShow();

            complete?.Invoke();
        });
    }

    protected virtual void OnScreenShow() {}


    public void Hide(Action complete)
    {
        if (_animator == null) return;

        _animator.Hide(() =>
        {
            _layout.OnHideLayout();

            OnScreenHide();

            complete?.Invoke();
        });
    }

    protected virtual void OnScreenHide() {}
}