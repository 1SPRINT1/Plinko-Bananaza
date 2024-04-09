using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effects : MonoBehaviour {

    private static Effects Instance { get; set; }
    
    [SerializeField] private ParticleEffect[] _effects;
    private readonly HashSet<ParticleEffect> _heap = new();
    
    private void Awake() => Instance = this;

    private bool TryGetEffect<T>(out T effect) where T : ParticleEffect {
        effect = _effects.OfType<T>().FirstOrDefault();

        return effect != null;
    }

    private void ExecuteEffect<T>(Vector3 position) where T : ParticleEffect {
        if (TryGetEffect<T>(out var effect)) {
            var instance = Instantiate(effect, position, Quaternion.identity, transform);
            
            instance.Execute(OnEffectDestroyed);
            
            _heap.Add(instance);
        }
    }
    private void OnEffectDestroyed(ParticleEffect effect) {
        _heap.Remove(effect);
        
        Destroy(effect.gameObject);
    }

    private void Clear() {
        _heap.Each(OnEffectDestroyed);
    }
    
    public static void Clean() => Instance.Clear();

    public static void Execute<T>(Vector3 position) where T : ParticleEffect => Instance.ExecuteEffect<T>(position);

}
