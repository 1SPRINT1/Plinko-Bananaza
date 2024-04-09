using UnityEngine;

public abstract class PresenterView : MonoBehaviour, IPresenterView {

    protected ScreenLayout Layout { get; private set; }
    public bool State => gameObject.activeInHierarchy;
    
    public void Construct(ScreenLayout layout) {
        Layout = layout;
    }

    public void Show() {
        // TODO: custom show flow
        OnPresenterShow();
    }
    protected virtual void OnPresenterShow() {}
    public void Hide() {
        // TODO: custom hide flow
        OnPresenterHide();
    }
    protected virtual void OnPresenterHide() {}

}
