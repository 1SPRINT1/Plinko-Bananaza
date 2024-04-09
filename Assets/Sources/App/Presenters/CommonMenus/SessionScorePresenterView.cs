﻿using TMPro;
using UnityEngine;

public class SessionScorePresenterView : PresenterView {
    [SerializeField] private TMP_Text _field;
    
    public void UpdateValue(int value) {
        _field.text = $"{value}";
    }
    
}
