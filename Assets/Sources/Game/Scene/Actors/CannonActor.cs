using System;
using DG.Tweening;
using UnityEngine;

public class CannonActor : MonoBehaviour {

    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _canon;
    
    private Sequence _sequence;
    private float _cooldown;
    private float _timer;

    public Transform Origin => _origin;
    public bool CanShot() => _timer < Time.time;
    
    public void Init(float cooldown) {
        _cooldown = cooldown;
    }
    
    public void RotateTowards(Vector3 position) {
        var direction = position - transform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
    }

    private Sequence FromSequence() {
        _sequence?.Kill();

        return _sequence = DOTween
            .Sequence();
    }

    private void Update() {
        
    }

    public void Release() {
        _timer = Time.time + _cooldown;
        
        Effects.Execute<PlayerShotEffect>(_origin.position);
        
        FromSequence()
            .Append(_canon.DOLocalMoveY(.2f, .25f).SetEase(Ease.OutBounce, 2))
            .Join(_canon.DOScaleY(.5f, .25f).SetEase(Ease.OutBounce, 2))
            .Join(_canon.DOScaleX(.5f, .25f).SetEase(Ease.OutBounce, 2))
            .Play();
    }
    
    public void Hold() {
        FromSequence()
            .Append(_canon.DOLocalMoveY(-0.1f, .25f))
            .Join(_canon.DOScaleY(.45f, .25f))
            .Join(_canon.DOScaleX(.55f, .25f))
            .Play();
    }
    
    
}
