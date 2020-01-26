using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Planet : MonoBehaviour
{
    [Header("Planet Properties")]
    public string Name;

    [Tooltip("Time for a planet to complete a rotation around its star")]
    public float OrbitPeriod = 365.25f;


    [Tooltip("Time for a planet to complete a rotation around itself")]
    public float PlanetDayPeriod = 24;

    public List<Satellite> Sattelites;

    protected SolarSystem _solarSystem;

    void Start()
    {
        _solarSystem = SolarSystem._Instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, 24/PlanetDayPeriod * _solarSystem.TimeScaleSettings.TimeScale * Time.deltaTime);
    }

}
