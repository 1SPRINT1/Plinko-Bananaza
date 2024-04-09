public static class UIKitExtensions {

    public static void OnButtonClick<TButton>(this ScreenLayout layout, IButtonCommand command) where TButton : ButtonElement
    {
        if (layout.TryGetElement<TButton>(out var button))
            button.OnClick(command);
    }

    public static void OnLabelShow<TLabel>(this ScreenLayout layout, ILabelCommand command) where TLabel : LabelElement
    {
        if (layout.TryGetElement<TLabel>(out var label))
            label.OnChange(command);
    }
    
    public static void OnInputShow<TLabel>(this ScreenLayout layout, ILabelCommand command) where TLabel : InputElement
    {
        if (layout.TryGetElement<TLabel>(out var label))
            label.OnChange(command);
    }

    public static void OnCheckerClick<TChecker>(this ScreenLayout layout, IButtonCommand command) where TChecker : CheckerElement
    {
        if (layout.TryGetElement<TChecker>(out var button))
            button.OnClick(command);
    }

    public static void OnSliderChange<TSlider>(this ScreenLayout layout, IValueCommand<float> command) where TSlider : SliderElement
    {
        if (layout.TryGetElement<TSlider>(out var slider))
            slider.OnChange(command);
    }

    public static void OnSelectFromGroup<TGroup>(this ScreenLayout layout, IGroupBoxCommand command) where TGroup : ButtonGroupElement
    {
        if (layout.TryGetElement<TGroup>(out var group))
            group.OnSelect(command);
    }

    public static void SetSelectedFromGroup<TGroup>(this ScreenLayout layout, byte index) where TGroup : ButtonGroupElement {
        if (layout.TryGetElement<TGroup>(out var group))
            group.SetElementSelected(index);
    }

}
