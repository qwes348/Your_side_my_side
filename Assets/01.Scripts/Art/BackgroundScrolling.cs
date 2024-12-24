using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    private Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (speed == 0)
            return;
        
        var offset = mat.mainTextureOffset;
        offset += Vector2.right * (Time.deltaTime * speed);
        mat.mainTextureOffset = offset;
    }
}
