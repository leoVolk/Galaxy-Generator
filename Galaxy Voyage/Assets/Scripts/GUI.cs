using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class UIComponents{
    public RectTransform SolarSystemInfoPanel;
    public TextMeshProUGUI SolarSystemName;
    public TextMeshProUGUI SolarSystemPlanets;
    public TextMeshProUGUI TimeScaleText;
}

public class GUI : MonoBehaviour
{
    public UIComponents UI;
    public Camera Camera;
    public GalaxyNavigator Navigator;
    protected SolarSystem _currentSolarSytem;

    // Start is called before the first frame update
    void Start()
    {
        UI.SolarSystemInfoPanel.gameObject.SetActive(false);
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)){
                SolarSystem solarSystem; 
                if(hit.collider.transform.parent.transform.parent.TryGetComponent<SolarSystem>(out solarSystem)){
                    if(_currentSolarSytem != solarSystem){
                        OnSolarSystemClicked(solarSystem);
                    }
                }
            }
        }
    }

    public void OnSolarSystemClicked(SolarSystem clickedSystem){
        _currentSolarSytem = clickedSystem;

        UI.SolarSystemInfoPanel.gameObject.SetActive(true);

        UI.SolarSystemName.text = _currentSolarSytem.Name;

        UI.SolarSystemPlanets.text = "<u>Planets:</u> \n"; 

        foreach(Planet p in _currentSolarSytem.Planets){
            UI.SolarSystemPlanets.text += "|-> "+ p.Name + "\n";
            foreach(Satellite s in p.Sattelites){
                UI.SolarSystemPlanets.text += "    |-> " + s.Name + "\n";
            }
        }

        
    }

    public void OnSolarSystemNavigate(){
        Navigator.NavigateToSolarSystem(_currentSolarSytem, Camera, _currentSolarSytem);
    }

    public void OnSolarSystemInspect(){

    }

    public void OnSolarSystemLeave(){
        UI.SolarSystemInfoPanel.gameObject.SetActive(false);
        Navigator.NavigateOutOfSolarSystem(Camera, _currentSolarSytem);
        _currentSolarSytem = null;
    }

    public void OnPlanetClicked(){

    }

    public void OnPlanetLeave(){

    }

    public void OnTimeScaleUpdate(float t){
        UI.TimeScaleText.text = t == 0 ? "1" : (t * 100000).ToString("#.##");
    }
}
