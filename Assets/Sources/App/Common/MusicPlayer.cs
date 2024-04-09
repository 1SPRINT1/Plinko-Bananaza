using UnityEngine;

public class MusicPlayer : AudioPlayer {
    [SerializeField] private AudioClip _clip;
    
    private static MusicPlayer Instance;

    private void Awake() {
        Instance = this;
    }
    
    private void PlayEffect() {
        PlayRepeat(_clip);
    }

    public static void Play() => Instance.PlayEffect();
    public static void FadeOut() => Instance.Fade();
    
}
