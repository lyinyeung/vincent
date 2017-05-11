using UnityEngine;
using System.Collections;

public class FirstPersonCam : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float minFov = 15f;
    public float maxFov = 90f;
    public float sensitivity = 10f;
    

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float fov;
    


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            yaw -= speedH * Input.GetAxis("Mouse X");
            pitch += speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        }

        fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
        
    }
}