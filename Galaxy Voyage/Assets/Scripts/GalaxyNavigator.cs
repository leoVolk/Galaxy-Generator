﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NavigationSettings{
    public float ZoomInSpeed = 4;
    public float ZoomOutSpeed = 4;

    public float ZoomIntensity = 10;
}

[System.Serializable]
public class LineRendererSettings{
    public AnimationCurve Curve;
    public Gradient Gradient;
}

[System.Serializable]
public class GalaxyNavigator : MonoBehaviour
{
    
    [SerializeField]
    public LineRendererSettings LineRendererSettings;
    
    public NavigationSettings NavigationSettings;


    private Galaxy _galaxy;

    // Start is called before the first frame update
    void Start()
    {
        _galaxy = GetComponent<Galaxy>();
    }


    public void NavigateToSolarSystem(SolarSystem solarSystem, Camera camera, SolarSystem currentSolarSystem){

        currentSolarSystem = solarSystem;

        camera.transform.position = new Vector3(currentSolarSystem.transform.position.x, currentSolarSystem.transform.position.y, -10);

        camera.GetComponent<CameraController>().Size = NavigationSettings.ZoomIntensity;
        camera.orthographicSize = camera.GetComponent<CameraController>().Size; 

        _galaxy.BlackHole.gameObject.SetActive(false);
        

        foreach(SolarSystem s in _galaxy.SolarSystems){
            if(s != currentSolarSystem){
                s.gameObject.SetActive(false);
            }else{
                s.GetComponent<LineRenderer>().widthMultiplier = 0;
                foreach(Planet p in s.Planets){
                    p.gameObject.SetActive(true);
                }
            }
        }
    }

    public void NavigateOutOfSolarSystem(Camera camera, SolarSystem currentSolarSystem){

        camera.GetComponent<CameraController>().Size = 65;
        camera.orthographicSize = 65; 
        
        foreach(SolarSystem s in _galaxy.SolarSystems){
            if(s != currentSolarSystem){
                s.gameObject.SetActive(true);
            }else{
                s.GetComponent<LineRenderer>().widthMultiplier = 1;
                foreach(Planet p in s.Planets){
                    p.gameObject.SetActive(false);
                }
            }
        }

        _galaxy.BlackHole.gameObject.SetActive(true);
    }
    
    public void Connect(List<SolarSystem> solarSystems){
        for(int i = 0; i < solarSystems.Count; i++){
            
            SolarSystem nearesSolarSystem;

            if(i == solarSystems.Count-1){
                nearesSolarSystem = solarSystems[0];
            }else{
                nearesSolarSystem = solarSystems[i+1];
            }

            //get nearest solar system
            for(int j = 0; j < solarSystems.Count; j++){
                if(j != i){
                    if(Vector3.Distance(solarSystems[i].transform.position, solarSystems[j].transform.position) < 
                        Vector3.Distance(solarSystems[i].transform.position, nearesSolarSystem.transform.position))
                        {
                            nearesSolarSystem = solarSystems[j];
                        }
                }
            }

            DrawLine(solarSystems[i], nearesSolarSystem.transform.position);
        }

    }

    private void DrawLine(SolarSystem currentSolarSystem, Vector3 b){
        LineRenderer lr = currentSolarSystem.gameObject.AddComponent<LineRenderer>();

        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        
        lr.colorGradient = LineRendererSettings.Gradient;
        lr.widthCurve = LineRendererSettings.Curve;

        lr.SetPosition(0, currentSolarSystem.transform.position);
        lr.SetPosition(1, b);
    }

    public void FollowPlanet(Camera camera){

    }

    public IEnumerator Zoom(Vector3 from, Vector3 to, Camera camera){
        //TODO: Find way to smoothly lerp into solar system
        yield return null;
    }
}
