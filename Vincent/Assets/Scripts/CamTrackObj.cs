using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamTrackObj : MonoBehaviour {

    public Transform mainCam;
    public Transform target;


	// Use this for initialization
	void Start () {
    }
   
    // Update is called once per frame
    void Update () {
        mainCam.LookAt(target);
	}
}
