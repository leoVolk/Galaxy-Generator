using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineRendererSettings{
    public AnimationCurve Curve;
    public Gradient Gradient;
}

[System.Serializable]
public class GalaxyNavigator : MonoBehaviour
{
    [SerializeField]
    public LineRendererSettings Settings;
    
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
        
        lr.colorGradient = Settings.Gradient;
        lr.widthCurve = Settings.Curve;

        lr.SetPosition(0, currentSolarSystem.transform.position);
        lr.SetPosition(1, b);
    }
}
