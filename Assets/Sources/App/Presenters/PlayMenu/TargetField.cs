using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetField : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private TMP_Text _targetAmount;
    [SerializeField] private Image _overhitImage;
    [SerializeField] private CanvasGroup _targetTitle;
    [SerializeField] private CanvasGroup _panel;

    [Space]
    [SerializeField] private Transform _endPosition; 
    [SerializeField] private Transform _startPosition;

    [Space]
    [SerializeField] private Image _slide;
    [SerializeField] private Transform _slideStart;
    [SerializeField] private Transform _slideEnd;
    private void OnEnable() {
        _panel.alpha = 0;
    }

    public void Present(int value) {
        _targetAmount.text = $"{value}";
        
        Dispose();
        
        DOTween
            .Sequence()
            .Append(_target.DOMove(_startPosition.position, 0))
            .Join(_target.DOScale(Vector3.one * 2f, 0))
            .Append(_panel.DOFade(1, .25f))
            .Append(_panel.transform.DOPunchScale(Vector3.one * .25f, .25f))
            .Join(_overhitImage.DOFade(1, .25f))
            
            .AppendInterval(1f)
            .Join(_overhitImage.DOFade(0, .5f).SetEase(Ease.Flash, 5, -.5f))
            
            .Append(_target.DOMove(_endPosition.position, .5f).SetEase(Ease.InOutBack))
            .Join(_target.DOScale(Vector3.one, .5f).SetEase(Ease.InOutBack))
            .Join(_targetTitle.DOFade(0, .5f))
            .AppendInterval(0.5f)
            .Append(_slide.DOFade(1, 0))
            .Append(_slide.transform.DOMove(_slideEnd.position, .25f).SetEase(Ease.InOutBounce))
            .Join(_slide.DOFade(0, .25f).SetEase(Ease.InOutBounce))
            .Play();
    }
    
    public void Dispose() {
        _panel.alpha = 0;
        _targetTitle.alpha = 1;
        _overhitImage.DOFade(0, 0);
        
        _slide.transform.DOMove(_slideStart.position, 0);
    }
}
