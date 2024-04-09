using System;
using UnityEngine;

public class TutorialMenuScreen : MenuScreen {
    [SerializeField] private GameObject[] _stages;
    
    private Action _onComplete;
    private int _currentStage;

    protected override void OnScreenShow() {
        _currentStage = -1;
        
        ShowNext();
    }

    public void OnComplete(Action onComplete) => _onComplete = onComplete;
    
    private void ShowNext() {
        _currentStage++;

        if (_currentStage >= _stages.Length) {
            _onComplete?.Invoke();
            return;
        }
        
        _stages.For((i, s) => {
            s.SetActive(i == _currentStage);
        });
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) ShowNext();
    }
}
