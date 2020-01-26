using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScaleSettings{
    public float TimeScale = 1;
}

public class SolarSystem : MonoBehaviour
{
    public static SolarSystem _Instance = null;
    public ScaleSettings TimeScaleSettings;

    [HideInInspector]
    public List<Planet> Planets;
    
    [HideInInspector]
    public Star Star;

    // Start is called before the first frame update
    void Awake()
    {
        if(_Instance == null){
            _Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Planet p in Planets){
            p.transform.RotateAround(Vector3.zero, Vector3.back, (((365.25f / p.OrbitPeriod)/360) * Time.deltaTime) * TimeScaleSettings.TimeScale);
        }
    }
}
