// Date   : 14.10.2017 11:02
// Project: Kajam#1
// Author : bradur

using UnityEngine;
using System.Collections;

public class LineLoopAnimation : MonoBehaviour {

    private Material material;

    [SerializeField]
    private string textureName = "linetext2";

    public float lineOffsetX = 0f;
    private Vector2 offset;

    void Start () {
        material = GetComponent<LineRenderer>().material;
        offset = material.mainTextureOffset;
    }

    void Update () {
        if (lineOffsetX != offset.x)
        {
            offset.x = lineOffsetX;
            material.mainTextureOffset = offset;
        }
    }
}
