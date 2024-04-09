using UnityEngine;

public class ObjScalerResolution : MonoBehaviour
{
    public Vector3 StartScaleObject;
    float ScaleStrnght = 680;
    Canvas GameCanvas;
    void Awake()
    {
       // StartScaleObject = gameObject.transform.localScale;
    }
    void Start()
    {
        GameCanvas = GetComponentInParent<Canvas>();// GetComponent<Canvas>();
        SetScaleObject();

    }


    void OnEnable()
    {
        SetScaleObject();
    }

    void SetScaleObject()
    {
     
        var ScaledWithResolution = StartScaleObject * GameCanvas.scaleFactor;


        gameObject.transform.localScale = ScaledWithResolution;
    }



}
