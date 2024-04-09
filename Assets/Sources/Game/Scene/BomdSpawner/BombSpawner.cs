using System;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour {
    [SerializeField] private BombActor _prefab;
    [SerializeField] private BombActor _playerPrefab;

    private readonly HashSet<BombActor> _actors = new();
    private Action _onPlayerHit;
    private Action _onPlayerIntercept;

    public void AddListener(Action onPlayerHit) {
        _onPlayerHit = onPlayerHit;
    }
    
    public void OnIntercept(Action onPlayerIntercept) {
        _onPlayerIntercept = onPlayerIntercept;
    }
    
    public void Spawn(Vector3 point, int direction, float speed) {
        var bomb = Instantiate(_prefab, point, Quaternion.Euler(Vector3.right * direction), transform);
        
        EffectsPlayer.Play(SoundEffectType.DropBomb);
        
        bomb.Force(Vector3.right * (-direction * speed));
        bomb.AddListener(OnBombDestroyed);
        
        _actors.Add(bomb);
    }
    
    private void OnBombDestroyed(BombActor actor, bool hitPlayer) {
        if (actor is PlayerBomb && hitPlayer) {
            _onPlayerIntercept?.Invoke();
        }
        
        if (actor is JetBomb && hitPlayer) {
            EffectsPlayer.Play(SoundEffectType.HitPlayer);
            _onPlayerHit?.Invoke();
        }
        
        _actors.Remove(actor);
        Destroy(actor.gameObject);
    }
    
    public void Dispose() {
        _actors.Each((a) => OnBombDestroyed(a, false));
    }
    public void Shoot(Vector3 origin, Vector3 direction) {
        var bomb = Instantiate(_playerPrefab, origin, Quaternion.Euler(direction), transform);
        
        bomb.Force(direction * 10);
        bomb.AddListener(OnBombDestroyed);
        
        _actors.Add(bomb);
    }
}
