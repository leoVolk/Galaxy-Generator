using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse
{
    public float XAxis;
    public float YAxis;

    public Ellipse (float _x, float _y){
        XAxis = _x;
        YAxis = _y;
    }

    public Vector2 Evaluate (float t){
        float angle = Mathf.Deg2Rad * 360 * t;
        float x = Mathf.Sin(angle) * XAxis;
        float y = Mathf.Cos(angle) * YAxis;

        return new Vector2(x, y);
    }
}
