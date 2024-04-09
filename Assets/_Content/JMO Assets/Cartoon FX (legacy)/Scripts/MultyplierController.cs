using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MultyplierController : MonoBehaviour
{
    [Header("X <= 3")]
    public Color colorLess3;
    [Range(-5f, 5f)]
    public float scrlSpeedLess3;

    [Header(" X 4")]
    public Color colorX4;
    [Range(-5f, 5f)]
    public float scrlSpeedX4;

    [Header(" X 5")]
    public Color colorX5;
    [Range(-5f, 5f)]
    public float scrlSpeedX5;

    [Header(" X > 5")]
    public Color colorGreater5;
    [Range(-5f, 5f)]
    public float scrlSpeedGreater5;

    [Space(1)]
    [Header("General Settings")]
    [Range(0f, 1f)]
    public float timing;


    MatUVAnimator _uvAnimator;

    void Awake()
    {
        _uvAnimator = GetComponent<MatUVAnimator>();
    }

    void SmoothChangeColor()
    {

    }

    void Update()
    {
    }
}
