using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HudScreen : MenuScreen {
    
    [SerializeField] private TMP_Text _levelField;
    [SerializeField] private CanvasGroup _levelPanel;
    [SerializeField] private CanvasGroup _hudPanel;
    [SerializeField] private CanvasGroup _screenBackground;
    [Space]
    [SerializeField] private TargetField _target;
    
    public void PresentLevel(string levelId, int score, Action onReady) {
        _levelField.text = $"{levelId}";
        
        Dispose();

        DOTween
            .Sequence()
            .Append(_levelPanel.DOFade(1, 1))
            .AppendInterval(1)
            .Append(_levelPanel.DOFade(0, 1))
            .AppendCallback(() => onReady?.Invoke())
            .AppendInterval(0.25f)
            .Append(_hudPanel.DOFade(1, .25f))
            .Join(_screenBackground.DOFade(0, .25f))
            
            .OnComplete(() => {
                _target.Present(score);
            })
            .Play();
    }

    public void Dispose() {
        _levelPanel.alpha = 0;
        _hudPanel.alpha = 0;
        _screenBackground.alpha = 1;
        _target.Dispose();
    }
}
