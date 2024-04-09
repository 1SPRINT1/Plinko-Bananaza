using System;
using UnityEngine;

public class PlayerShotEffect : ParticleEffect {

    [SerializeField] private ParticleSystem _effect;
    
    public override void Execute(Action<ParticleEffect> onEffectDestroyed) {
        var delay = _effect.main.duration + 2;
        
        _effect.Play();

        EffectsPlayer.Play(SoundEffectType.PlayerShot);

        Delay.Execute(delay, () => onEffectDestroyed?.Invoke(this));
    }
}
