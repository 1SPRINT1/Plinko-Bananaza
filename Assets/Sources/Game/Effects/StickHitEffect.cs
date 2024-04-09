using System;
using UnityEngine;

public class StickHitEffect : ParticleEffect {

    [SerializeField] private ParticleSystem _effect;
    public override void Execute(Action<ParticleEffect> onEffectDestroyed) {
        var delay = _effect.main.duration;
        
        _effect.Play();

        Delay.Execute(delay, () => onEffectDestroyed?.Invoke(this));
    }
}