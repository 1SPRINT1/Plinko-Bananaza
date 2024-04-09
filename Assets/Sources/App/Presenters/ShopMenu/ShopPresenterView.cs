using UnityEngine;
using UnityEngine.UI;

public class ShopPresenterView : PresenterView {

    [SerializeField] private Image _fillProgress;
    
    public void SetProgress(float progress) {
        _fillProgress.fillAmount = Mathf.Clamp01(progress);
    }
    
}
