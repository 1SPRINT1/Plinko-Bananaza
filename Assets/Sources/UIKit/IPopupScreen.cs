using System;

public interface IPopupScreen
{
    void OnComplete(Action complete);

    void Show(Action complete);
    void Hide(Action complete);
}
