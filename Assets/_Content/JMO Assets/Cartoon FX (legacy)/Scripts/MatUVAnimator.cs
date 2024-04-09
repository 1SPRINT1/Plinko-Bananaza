using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatUVAnimator : MonoBehaviour
{
    public Material AnimatedMaterial;

    public float scrollSpeed  = 0.5f;

   // Vector2 offset;

    void Start()
    {
       // offset = AnimatedMaterial.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        if(AnimatedMaterial.GetTextureOffset("_MainTex").x > 10)
        {
            AnimatedMaterial.SetTextureOffset("_MainTex", new Vector2(0,0));
        }
        AnimatedMaterial.SetTextureOffset("_MainTex", new Vector2(Time.time * scrollSpeed, 0));
    }
}
