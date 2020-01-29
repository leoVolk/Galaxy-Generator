using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GalaxyGenerator))]
public class Galaxy : SpaceObject
{    
    public BlackHole BlackHole;
    public List<SolarSystem> SolarSystems;
    public float TimeScale = 1;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        foreach(SolarSystem s in SolarSystems){
            s.TimeScaleSettings.TimeScale = TimeScale;
        }
    }
}
