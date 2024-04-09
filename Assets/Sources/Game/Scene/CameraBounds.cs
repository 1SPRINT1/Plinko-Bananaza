using NaughtyAttributes;
using UnityEngine;

public class CameraBounds : MonoBehaviour {

    [SerializeField] private Camera _camera;
    
    [Button]
    private void AlignToCamera() {
        var ratio = Screen.width / (float)Screen.height;
        var height = _camera.orthographicSize * 2;
        var width = height * ratio;
        Debug.Log($"Size width/height: {width}/{height}");
        //var rect = _camera.orthographicSize
    }
    
}

public static class CameraExtensions {
    public static Vector2 GetCameraProjection(this Camera camera) {
        var ratio = Screen.width / (float)Screen.height;
        var height = camera.orthographicSize * 2;
        var width = height * ratio;

        return new Vector2(width, height);
    }
}
