using System;
using DG.Tweening;
using UnityEngine;

public class RollAnimator : LayoutAnimator {

    [SerializeField] private float _fadeTime = .5f;

    public override void Show(Action complete)
    {
        gameObject.SetActive(true);

        DOTween
            .Sequence()
            .Append(CanvasGroup.DOFade(0, 0))
            .Append(CanvasGroup.DOFade(1, _fadeTime))
            .OnComplete(() => complete?.Invoke())
            .Play();
    }

    public override void Hide(Action complete)
    {
        DOTween
            .Sequence()
            .Append(CanvasGroup.DOFade(1, 0))
            .Append(CanvasGroup.DOFade(0, _fadeTime))
            .OnComplete(() => {
                complete?.Invoke();
                gameObject.SetActive(false);
            })
            .Play();
    }
}
