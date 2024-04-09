using UnityEngine;

public class GameLevel : MonoBehaviour {

    [SerializeField] private StickPlacement _sticks;
    public void Represent() {
        _sticks.Represent();
    }

    public void Construct(LevelTemplate template) {
        _sticks.PlaceSticks(template.Rows, template.Cols);
    }
    
    public void Dispose() {
        _sticks.Clear();
    }
}
