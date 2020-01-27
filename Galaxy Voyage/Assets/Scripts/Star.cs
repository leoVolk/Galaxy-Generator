using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("Star Properties")]
    public string Name;
    
    public Vector3 Size;

    public Gradient Color;

    private Transform _body;

    void Start()
    {
        _body = transform.GetChild(0);
        _body.transform.localScale = Size;

        _body.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.Evaluate(Size.x/4));
        _body.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.Evaluate(Size.x/4));
    }
}
