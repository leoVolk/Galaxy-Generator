using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GalaxyGenerator))]
public class Galaxy : MonoBehaviour
{
    public string Name;
    public Vector3 Position;
    public List<SolarSystem> SolarSystems;

}
