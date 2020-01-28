using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GalaxyGenerator))]
public class Galaxy : SpaceObject
{    public BlackHole BlackHole;
    public List<SolarSystem> SolarSystems;

}
