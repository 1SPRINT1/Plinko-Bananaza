using DG.Tweening;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    [SerializeField] private AudioSource _source;

    private void OnValidate() => _source = GetComponent<AudioSource>();

    public void ChangeState(bool isEnabled) => _source.mute = !isEnabled;

    public void PlayOnce(AudioClip clip) => _source.PlayOneShot(clip);

    public void SetVolume(float amount) => _source.volume = Mathf.Clamp01(amount);

    public void PlayRepeat(AudioClip clip) {
        
        DOTween
            .Sequence()
            .Append(_source.DOFade(0, 0))
            .Append(_source.DOFade(1, .25f))
            .OnComplete(() =>
                {
                    _source.clip = clip;
                    _source.Play();    
                })
            .Play();
        
    }

    protected void Fade() {
        _source.DOFade(0, .25f).OnComplete(StopAll);
    }

    public void StopAll() => _source.Stop();
}