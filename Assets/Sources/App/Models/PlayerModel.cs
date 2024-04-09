using UnityEngine;

public enum PlayerButtonState { None, Hold, Release }

public class PlayerModel : IUIKitModel {
    private readonly LevelTemplate[] _templates;

    private const int LIVES_DEFAULT = 5;
    private const int MAX_COOLDOWN_LEVEL = 4;
    private const int COOLDOWN_LEVEL_PRICE = 50;
    
    public IObservableField<float> Volume { get; } = new ObservableFloat(1);
    public IObservableField<int> CooldownLevel { get; } = new ObservableInt(0);
    public IObservableField<int> CurrentLevel { get; } = new ObservableInt(0);
    public IObservableField<int> Score { get; } = new ObservableInt(0);
    
    
    // Session value;
    public IObservableField<PlayerButtonState> ButtonState { get; } = new ObservableEnum<PlayerButtonState>();
    public IObservableField<int> SessionScore { get; } = new ObservableInt(0);
    public IObservableField<int> Lives { get; } = new ObservableInt(LIVES_DEFAULT);
    public IObservableField<Vector3> AimPoint { get; } = new ObservableStruct<Vector3>(Vector3.zero, updateAlways: true);
    public LevelTemplate Template => _templates[Mathf.Clamp(CurrentLevel.Value, 0, _templates.Length)];
    public bool CooldownUpgradeIsPossible => CooldownLevel.Value < MAX_COOLDOWN_LEVEL && Score.Value >= COOLDOWN_LEVEL_PRICE;
    public bool TutorialComplete { get; set; }

    private float _cooldown;
    public PlayerModel(LevelTemplate[] templates) {
        _templates = templates;
        // TODO: This feature require to replace into the separated class;
        Load();
    }
    
    public bool GetLevelState() {
        return Lives.Value > 0 && SessionScore.Value >= Template.TargetScore;
    }
    
    public void ResetSession() {
        ButtonState.Value = PlayerButtonState.None;
        Lives.Value = LIVES_DEFAULT;
        SessionScore.Value = 0;
    }

    public void ApplySession() {
        Score.Value += SessionScore.Value;
    }
    
    public bool UpgradeCooldown() {
        Score.Value -= COOLDOWN_LEVEL_PRICE;
        
        CooldownLevel.Value += 1;
        
        return CooldownUpgradeIsPossible;
    }
    
    private void Load() {
        Volume.Value = ES3.KeyExists(nameof(Volume)) ? ES3.Load<float>(nameof(Volume)) : 1;
        CooldownLevel.Value = TryLoad<int>(nameof(CooldownLevel));
        Score.Value = TryLoad<int>(nameof(Score));
        CurrentLevel.Value = TryLoad<int>(nameof(CurrentLevel));
    }

    private T TryLoad<T>(string key) {
        if (ES3.KeyExists(key)) return ES3.Load<T>(key);

        return default;
    }
    
    public void Save() {
        ES3.Save(nameof(Volume), Volume.Value);
        ES3.Save(nameof(CooldownLevel), CooldownLevel.Value);
        ES3.Save(nameof(Score), Score.Value);
        ES3.Save(nameof(CurrentLevel), CurrentLevel.Value);
    }


    
}
