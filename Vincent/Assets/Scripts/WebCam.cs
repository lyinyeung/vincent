// Starts the default camera and assigns the texture to the current renderer
using UnityEngine;
using System.Collections;

public class WebCam : MonoBehaviour
{
    void Start()
    {
        Application.RequestUserAuthorization(UserAuthorization.WebCam);

        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}