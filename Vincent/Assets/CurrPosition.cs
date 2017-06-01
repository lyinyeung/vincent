using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrPosition : MonoBehaviour {

    public Transform mainCam;
    public Text currPosTxt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float dec = mainCam.rotation.eulerAngles.z;

        currPosTxt.text = dec.ToString("F2");
        
        	
	}
}
