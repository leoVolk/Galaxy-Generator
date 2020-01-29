using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraSettings{
    public float ZoomSpeed = 4f;
    public float MinZoom = 0f;
    public float MaxZoom = 15f;
    public float MovementSpeed = 4f;
    public bool InvertY = true;
    public bool InvertX = true;
}

public class CameraController : MonoBehaviour
{
    
    public CameraSettings Settings;
    private Camera _camera;
    public float Size = 5;

    private LineRenderer[] _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
        _lineRenderer = GameObject.FindObjectsOfType<LineRenderer>();

        Size = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Mouse ScrollWheel") > 0){
            Size -= Input.GetAxis("Mouse ScrollWheel") * Settings.ZoomSpeed;
            Size = Mathf.Clamp(Size, Settings.MinZoom, Settings.MaxZoom);


            _camera.orthographicSize = Size;
        }

        if(Input.GetKey(KeyCode.Mouse2) || Input.GetKey(KeyCode.Mouse1))
        {
            float x = Settings.InvertX 
                        ? -Input.GetAxis("Mouse X") *Time.deltaTime * Settings.MovementSpeed * Size 
                        : Input.GetAxis("Mouse X") *Time.deltaTime * Settings.MovementSpeed * Size;
                        
            float y = Settings.InvertY 
                        ? -Input.GetAxis("Mouse Y") *Time.deltaTime * Settings.MovementSpeed * Size 
                        : Input.GetAxis("Mouse Y") *Time.deltaTime * Settings.MovementSpeed * Size;

            transform.Translate(new Vector3(x, y, 0));
        }
    }
}
