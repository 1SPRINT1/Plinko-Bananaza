using System;
using UnityEngine;

public class SelfDestroyEffect : ParticleEffect {

    [SerializeField] private ParticleSystem _effect;
    
    public override void Execute(Action<ParticleEffect> onEffectDestroyed) {
        var delay = _effect.main.duration;
        
        _effect.Play();
        
        EffectsPlayer.Play(SoundEffectType.HitPlayer);
        
        Delay.Execute(delay, () => onEffectDestroyed?.Invoke(this));
    }
}
