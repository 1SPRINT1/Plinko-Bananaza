using System;
using DG.Tweening;
using UnityEngine;

public class DefaultAnimator : LayoutAnimator {
    [SerializeField] private float _fadeInTime = .25f;
    
    private Sequence _sequence;
    
    public override void Show(Action complete) {
        CanvasGroup.alpha = 0;
        gameObject.SetActive(true);
        _sequence?.Kill();
        
        _sequence = DOTween
            .Sequence()
            .Append(CanvasGroup.DOFade(1, _fadeInTime))
            .OnComplete(() => complete?.Invoke())
            .Play();
    }

    public override void Hide(Action complete)
    {
        CanvasGroup.alpha = 1;
        _sequence?.Kill();
        
        _sequence = DOTween
            .Sequence()
            .Append(CanvasGroup.DOFade(0, _fadeInTime))
            .OnComplete(() => {
                gameObject.SetActive(false);
                complete?.Invoke();
                
                _sequence.Kill();
            })
            .Play();
    }
}
