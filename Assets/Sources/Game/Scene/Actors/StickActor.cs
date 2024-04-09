using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class StickActor : MonoBehaviour {

    [SerializeField] private SpriteRenderer _renderer;

    private void OnValidate() {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.DOFade(0, 0);
    }

    private void Awake() {
        _renderer.DOFade(0, 0);
    }

    public void Present(int index) {
        Delay.Execute(.05f * index, () => {
            DOTween
                .Sequence()
                .Append(_renderer.DOFade(1, .2f))
                .Append(transform.DOPunchScale(Vector3.one * .1f, .1f))
                .Play();
        });
    }
    
    public void PresentOf(int index) {
        Delay.Execute(.05f * index, () => {
            DOTween
                .Sequence()
                .Append(transform.DOPunchScale(Vector3.one * .1f, .25f))
                .Play();
        });
    }
    
    public void Apply() {
        Effects.Execute<StickHitEffect>(transform.position);
        EffectsPlayer.Play(SoundEffectType.HitStick);
        
        DOTween
            .Sequence()
            .Append(transform.DOPunchScale(Vector3.one * .25f, .25f))
            .Append(transform.DOScale(Vector3.one * .25f, 0.25f))
            .Play();
    }
}