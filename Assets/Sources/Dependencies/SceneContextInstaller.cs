using UnityEngine;

public class SceneContextInstaller : EntryUIKitInstaller {
    
    [SerializeField] private AudioPlayer[] _players;
    [SerializeField] private LevelTemplate[] _templates;
    
    protected override void BindServices() {
        Container.BindAsSingleFromInstance(_templates);
        _players.Each(p => Container.BindAsSingleFromInstance(p));
        
        Container.BindAsSingleFromInstanceMono<ScenePresenter>();
        Container.BindAsSingle<GameController>();
    }

    protected override void OnBindTargetInstances() {
        
    }

}