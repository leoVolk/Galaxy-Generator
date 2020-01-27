using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("Star Properties")]
    public string Name;
    
    public Vector3 Size;

    private Transform _body;

    void Start()
    {
        _body = transform.GetChild(0);
        _body.transform.localScale = Size;
    }
}
