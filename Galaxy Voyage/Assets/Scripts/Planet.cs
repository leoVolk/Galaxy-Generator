using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class Planet : SpaceObject
{
    [Header("Planet Properties")]

    [Tooltip("Time for a planet to complete a rotation around its star")]
    public float OrbitPeriod = 365.25f;


    [Tooltip("Time for a planet to complete a rotation around itself")]
    public float PlanetDayPeriod = 24;

    public List<Satellite> Sattelites;

    protected SolarSystem _solarSystem;
    protected Transform _body;

    void Start()
    {
        _body = transform.Find("Body");
        _solarSystem = SolarSystem._Instance;
        _body.transform.localScale = Size;

        transform.name = Name;


        transform.Find("Name_Text").GetComponent<TextMeshPro>().text = this.Name;
    }

    // Update is called once per frame
    void Update()
    {
        _body.transform.Rotate(Vector3.back, 24/PlanetDayPeriod * _solarSystem.TimeScaleSettings.TimeScale * Time.deltaTime);
    }

}
