using UnityEngine;

[CreateAssetMenu(menuName = "Create LevelTemplate", fileName = "LevelTemplate", order = 0)]
public class LevelTemplate : ScriptableObject {
    
    [SerializeField] private string _levelTitle;
    [SerializeField] private int _targetScore;

    [Space]
    [SerializeField] private int _cols;
    [SerializeField] private int _rows;
    
    [Space]
    [SerializeField] private AudioClip _ambientMusic;
    [SerializeField] private GameLevel _prefab;

    public string LevelTitle => _levelTitle;
    public int TargetScore => _targetScore;

    public int Rows => _rows;
    public int Cols => _cols;
    
    public GameLevel Create(Transform content) {
        var instance = Instantiate(_prefab, content);

        instance.Construct(this);
        
        return instance;
    }
}