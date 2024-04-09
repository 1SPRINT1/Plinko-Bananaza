using System;
using System.Linq;
using UnityEngine;

public enum SystemEffectType { None, ButtonClick }

[Serializable]
public class SystemEffect {
    public SystemEffectType type;
    public AudioClip clip;
}

public class SystemPlayer : AudioPlayer {
    [SerializeField] private SystemEffect[] _effects;
    
    private static SystemPlayer Instance;

    private void Awake() {
        Instance = this;
    }

    private void PlayEffect(SystemEffectType type) {
        var effect = _effects.FirstOrDefault(e => e.type == type);
        
        if(effect != null) PlayOnce(effect.clip);
    }

    public static void Play(SystemEffectType type) => Instance.PlayEffect(type);
}
