using System;
using UnityEngine;
using Zenject;

public class AudioPresenter : MonoBehaviour {
    [SerializeField] private AudioPlayer[] _players;
    
    private PlayerModel _model;
    
    [Inject]
    private void Construct(PlayerModel model) => _model = model;

    private void OnEnable() => _model.Volume.Changed += OnVolumeChanged;
    private void OnVolumeChanged(float volume) => _players.Each(p => p.SetVolume(volume));

    private void OnDisable() => _model.Volume.Changed -= OnVolumeChanged;
}
