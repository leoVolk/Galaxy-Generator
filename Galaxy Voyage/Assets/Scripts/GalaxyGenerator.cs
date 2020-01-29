using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[System.Serializable]
public class GalaxyPrefabs{
    public GameObject SolarSystemPrefab;
    public GameObject BlackHolePrefab;
}


[System.Serializable]
public class GalaxyGenerationSettings{
    [Range(0, 1000)]
    public float Radius = 100;

    [Range(1, 100)]
    public int MinGalaxies = 10;

    [Range(1, 100)]
    public int MaxGalaxies = 100;

    [Range(10, 50)]
    public float MinDistanceBetweenGalaxy = 10;
}


public class GalaxyGenerator : MonoBehaviour
{
    public GalaxyPrefabs GalaxyPrefabs;
    public GalaxyGenerationSettings Settings;

    private Galaxy _galaxy;
    void Awake(){
        _galaxy = GetComponent<Galaxy>();

        GenerateGalaxy();
    }

    [Button("Generate Galaxy")]
    public void GenerateGalaxy(){

        if(_galaxy.SolarSystems != null){
            foreach(SolarSystem g in _galaxy.SolarSystems){
                Destroy(g.gameObject);
            }

            _galaxy.SolarSystems.Clear();

            if(_galaxy.BlackHole != null){
                Destroy(_galaxy.BlackHole.gameObject);
            }
        }


        GameObject blackHoleGameObject = Instantiate(GalaxyPrefabs.BlackHolePrefab, transform.position, Quaternion.identity, this.transform);
        BlackHole blackHole = blackHoleGameObject.GetComponent<BlackHole>();
        _galaxy.BlackHole = blackHole;

        //Set galaxy name
        System.Random rand = new System.Random();
        _galaxy.Name = StarName.Generate(rand);
        this.name = _galaxy.Name;

        _galaxy.Size = new Vector2(Settings.Radius, Settings.Radius);

        int solarSystems = Random.Range(Settings.MinGalaxies, Settings.MaxGalaxies);

        //Generate solar systems of galaxy
        for(int i = 0; i  < solarSystems; i++){

            Vector2 pos = Random.insideUnitCircle * Settings.Radius;

            while(CheckForGalaxies(pos)){
                pos = Random.insideUnitCircle * Settings.Radius;
            }

            GameObject solarSystemGameObject = Instantiate(GalaxyPrefabs.SolarSystemPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity, this.transform);

            SolarSystemGenerator solarSystemGenerator = solarSystemGameObject.GetComponent<SolarSystemGenerator>();
            SolarSystem solarSystem = solarSystemGameObject.GetComponent<SolarSystem>();

            solarSystem.Name = StarName.Generate(rand);
            solarSystemGameObject.name = solarSystem.Name;

            solarSystem.Position = solarSystemGameObject.transform.position;

            _galaxy.SolarSystems.Add(solarSystem);

            solarSystemGenerator.GenerateSolarSystem();

            foreach(Planet p in solarSystem.Planets){
                p.gameObject.SetActive(false);
            }
        }

        GetComponent<GalaxyNavigator>().Connect(_galaxy.SolarSystems);

    }

    public bool CheckForGalaxies(Vector2 v){
        return Physics.OverlapSphere(v, Settings.MinDistanceBetweenGalaxy).Length > 0;
    }
}
