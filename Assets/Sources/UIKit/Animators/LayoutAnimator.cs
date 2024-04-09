using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class LayoutAnimator : MonoBehaviour
{
    [SerializeField, HideInInspector] private CanvasGroup _canvas;

    protected CanvasGroup CanvasGroup => _canvas;
    
    private void OnValidate()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    public abstract void Show(Action complete);

    public abstract void Hide(Action complete);

}