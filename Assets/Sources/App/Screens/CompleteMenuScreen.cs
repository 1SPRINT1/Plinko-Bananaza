using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompleteMenuScreen : MenuScreen {

    [SerializeField] private GameObject _complete;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private TMP_Text _continueField;
    [SerializeField] private TMP_Text _scoreField;

    public void PostScreen(bool isWin, int before, int score) {
        _complete.SetActive(isWin);
        _gameOver.SetActive(!isWin);

        var title = isWin ? "Continue" : "Exit";
        _continueField.text = title;

        _scoreField.text = before.ToString();
        _scoreField.DOCounter(before, score, 1f);
    }

}
