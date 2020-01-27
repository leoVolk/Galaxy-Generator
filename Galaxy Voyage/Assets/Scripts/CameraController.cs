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


    private float _size = 5;

    private LineRenderer[] _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
        _lineRenderer = GameObject.FindObjectsOfType<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Mouse ScrollWheel") > 0){
            _size -= Input.GetAxis("Mouse ScrollWheel") * Settings.ZoomSpeed;
            _size = Mathf.Clamp(_size, Settings.MinZoom, Settings.MaxZoom);

            foreach(LineRenderer lr in _lineRenderer){
                lr.startWidth = (lr.startWidth * _size)*.25f;
                lr.endWidth  = (lr.endWidth * _size)*.25f;
            }

            _camera.orthographicSize = _size;
        }

        if(Input.GetKey(KeyCode.Mouse2) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse0))
        {
            float x = Settings.InvertX 
                        ? -Input.GetAxis("Mouse X") *Time.deltaTime * Settings.MovementSpeed * _size 
                        : Input.GetAxis("Mouse X") *Time.deltaTime * Settings.MovementSpeed * _size;
                        
            float y = Settings.InvertY 
                        ? -Input.GetAxis("Mouse Y") *Time.deltaTime * Settings.MovementSpeed * _size 
                        : Input.GetAxis("Mouse Y") *Time.deltaTime * Settings.MovementSpeed * _size;

            transform.Translate(new Vector3(x, y, 0));
        }
    }
}
