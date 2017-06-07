using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrPosition : MonoBehaviour {

    public Transform mainCam;
    public GameObject mainCamObj;
    public Text currPosTxt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float dec = mainCam.rotation.eulerAngles.x;
        float ra = mainCam.rotation.eulerAngles.y;
        ra = (360 - ra) / 15;

        if (dec > 0 && dec <= 90)
            dec = -dec;

        if (dec <= 360 && dec >= 270)
            dec = 360 - dec;

             
        
        currPosTxt.text = dec.ToString("F2") + "         " + ra.ToString("F2");
        
        	
	}
}
