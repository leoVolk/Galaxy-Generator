using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : Planet
{
    public Planet Orbiter;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Orbiter.transform.position, Vector3.back, ((365.25f / OrbitPeriod) * Time.deltaTime) * _solarSystem.TimeScaleSettings.TimeScale);
    }
}
