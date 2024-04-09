using System;
using System.Linq;
using UnityEngine;

public enum SoundEffectType { None, HitPlayer, HitStick, DropBomb, PlayerShot }

[Serializable]
public class SoundEffect {
    public SoundEffectType type;
    public AudioClip clip;
}

public class EffectsPlayer : AudioPlayer {
    [SerializeField] private SoundEffect[] _effects;
    
    private static EffectsPlayer Instance;

    private void Awake() {
        Instance = this;
    }

    private void PlayEffect(SoundEffectType type) {
        var effect = _effects.FirstOrDefault(e => e.type == type);
        
        if(effect != null) PlayOnce(effect.clip);
    }

    public static void Play(SoundEffectType type) => Instance.PlayEffect(type);

}

