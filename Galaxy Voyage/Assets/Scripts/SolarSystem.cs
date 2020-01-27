﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScaleSettings{
    public float TimeScale = 1;
}

public class SolarSystem : MonoBehaviour
{
    [Header("Solar System Properties")]
    public string Name;

    public Vector3 Position;

    public static SolarSystem _Instance = null;
    public ScaleSettings TimeScaleSettings;

    public List<Planet> Planets;
    
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
            if(p.gameObject.activeSelf){
                p.transform.RotateAround(transform.position, Vector3.back, (((365.25f / p.OrbitPeriod)/360) * Time.deltaTime) * TimeScaleSettings.TimeScale);
            }
        }
    }

    public void AllignPlanets(){
        foreach(Planet p in Planets){
            p.transform.GetChild(0).GetChild(0).GetComponent<TrailRenderer>().enabled = false;
            p.transform.RotateAround(transform.position, Vector3.back, Random.Range(0, 360));
            p.transform.GetChild(0).GetChild(0).GetComponent<TrailRenderer>().enabled = true;
        }
    }
}
