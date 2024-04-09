using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StickPlacement : MonoBehaviour {
    
    [SerializeField] private StickActor _prefab;
    private readonly HashSet<StickActor> _sticks = new();
    
    public void PlaceSticks(int rows, int cols) {
        var size = Camera.main.GetCameraProjection();
        
        var horizontalOffset = (size.x) / cols;
        var verticalOffset = (size.y  * .25f) / rows;

        var worldOffset = Vector3.right * (horizontalOffset * .5f) + 
            Vector3.left * (horizontalOffset * cols * .5f) + 
            Vector3.down * (verticalOffset * rows * .5f);

        for (int x = 0; x < cols; x++) {
            for (int y = 0; y < rows; y++) {
                if(y % 2 == 0 && x + 1 == cols) continue;
                
                var odd = y % 2 == 0 ? horizontalOffset * .5f : 0;
                var stickPosition = new Vector3(
                    x * horizontalOffset + odd,
                    y * verticalOffset
                    );

                var stick = Instantiate(_prefab, stickPosition + worldOffset, Quaternion.identity, transform);
                
                _sticks.Add(stick);
            }
        }

        _sticks.Shuffle(5).For((i,s) => s.Present(i));
    }

    [Button]
    private void TestPlace() {
        PlaceSticks(4, 4);
    }
    
    [Button]
    public void Clear() {
        _sticks.Each(a => DestroyImmediate(a.gameObject));
    }
    
    public void Represent() {
        _sticks.Shuffle(5).For((i,s) => s.PresentOf(i));
    }
}
