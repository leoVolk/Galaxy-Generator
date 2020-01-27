using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class SolarSystemPrefabs{
    public GameObject StarPrefab;
    public GameObject PlanetPrefab;
    public GameObject SattelitePrefab;
}

[System.Serializable]
public class SolarSystemGenerationSetting{
    
    [HideInInspector]
    public float SizeScale = .01f;
    public int MaxPlanets = 10;
    public int MinPlanets = 6; 

    [Range(0, 10)]
    public int ChanceOfSatellite = 2;

    [Range(0, 10)]
    public int MaxSatellites = 10;

    [Range(0, 10)]
    public int MinSatellites = 1;

    [Tooltip("100 = 10.000.000 km")]
    public float MinDistanceBetween = 100;

    [Tooltip("1000 = 10.000.000.000 km")]
    public float MaxDistanceBetween = 1000;

    [Range(10, 100)]
    public float MaxStarSize = 100;
    [Range(10, 100)]
    public float MinStarSize = 10;

    [Range(0.5f, 10)]
    public float MaxPlanetSize = 10;

    [Range(0.5f, 10)]
    public float MinPlanetSize = .5f;
}

public class SolarSystemGenerator : MonoBehaviour
{
    public SolarSystemGenerationSetting Settings;
    public SolarSystemPrefabs SolarSystemPrefabs;

    private SolarSystem _solarSystem;
    // Start is called before the first frame update
    void Awake()
    {
        _solarSystem = GetComponent<SolarSystem>();
    }

    
    [Button("Generate Solar System")]
    public void InitSolarSystem(){
        System.Random rand = new System.Random();

        //for generating a new solar system on the fly
        if(_solarSystem.Planets != null){
            foreach(Planet p in _solarSystem.Planets){
                Destroy(p.gameObject);
            }
            _solarSystem.Planets.Clear();
        }


        if(_solarSystem.Star != null){
            Destroy(_solarSystem.Star.gameObject);
        }

        GameObject starGameObject = Instantiate(SolarSystemPrefabs.StarPrefab, transform.position, Quaternion.identity, this.transform);

        Star star = starGameObject.GetComponent<Star>();
            
        star.Name = StarName.Generate(rand);

        float starSize = Random.Range(Settings.MinStarSize, Settings.MaxStarSize) * Settings.SizeScale;
        star.Size = new Vector3(starSize, starSize, starSize);

        _solarSystem.Star = star;
            

        int planetAmount = Random.Range(Settings.MinPlanets, Settings.MaxPlanets);
        float lastPlanetDistance = 0f;

        for(int i = 0; i < planetAmount; i++){

            //Instantiate planets
            float distance = Random.Range(Settings.MinDistanceBetween, Settings.MaxDistanceBetween) + lastPlanetDistance;
            lastPlanetDistance = distance;
            
            GameObject planetGameObject = Instantiate(SolarSystemPrefabs.PlanetPrefab, 
                                            new Vector3(distance * Settings.SizeScale, distance * Settings.SizeScale, 0), 
                                            Quaternion.identity, this.transform);


            Planet planet = planetGameObject.GetComponent<Planet>();

            //set Planet Name
            planet.Name = StarName.Generate(rand);

            //set Planet Size
            float planetSize = Random.Range(Settings.MinPlanetSize, Settings.MaxPlanetSize) * ((distance/10000)/Settings.SizeScale);
            planet.Size = new Vector3(planetSize,planetSize,planetSize);

            planet.OrbitPeriod = (distance / Settings.SizeScale) * 2;
            _solarSystem.Planets.Add(planet);



            //determine if planet has satellites orbiting it and instantiate them
            bool hasSatellite = Random.Range(Settings.ChanceOfSatellite, 10) <= Settings.ChanceOfSatellite;

            if(hasSatellite){
                int satelliteAmount = Random.Range(Settings.MinSatellites, Settings.MaxPlanets);

                float lastSatelliteDistance = 0f;

                for(int j = 0; j < satelliteAmount; j++){
                    float distanceToPlanet = (((distance * Settings.SizeScale) + lastSatelliteDistance) + planetSize) / 20;

                    lastSatelliteDistance = distanceToPlanet;
                    
                    GameObject satelliteGameObject = Instantiate(SolarSystemPrefabs.SattelitePrefab, 
                                                        planetGameObject.transform.position + new Vector3(distanceToPlanet, 0, distanceToPlanet),
                                                        Quaternion.identity, planet.transform);

                    Satellite satellite = satelliteGameObject.GetComponent<Satellite>();
                    satellite.OrbitPeriod = distanceToPlanet * planetSize * 100;
                    satellite.Orbiter = planet;

                    //set Satellite name
                    satellite.Name = StarName.Generate(rand);
                    
                    float satelliteSize = Random.Range(Settings.MinPlanetSize, Settings.MaxPlanetSize) * ((distanceToPlanet/1000)/Settings.SizeScale);
                    satellite.Size = new Vector3(satelliteSize, satelliteSize, satelliteSize);

                    planet.Sattelites.Add(satellite);
                }
            }
        }
    
        _solarSystem.AllignPlanets();
    }
}
