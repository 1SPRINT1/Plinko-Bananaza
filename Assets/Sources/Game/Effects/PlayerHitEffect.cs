using System;
using DG.Tweening;
using UnityEngine;

public class PlayerHitEffect : ParticleEffect {

    [SerializeField] private ParticleSystem _effect;
    public override void Execute(Action<ParticleEffect> onEffectDestroyed) {
        var delay = _effect.main.duration;
        
        _effect.Play();

        var main = Camera.main;

        DOTween
            .Sequence()
            .Append(main.transform.DOShakePosition(0.25f, Vector3.one * .1f))
            .Play();

        Delay.Execute(delay, () => onEffectDestroyed?.Invoke(this));
    }
}