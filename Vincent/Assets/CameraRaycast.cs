using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour {

    public Text nameText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * 550;
        Debug.DrawRay(new Vector3(0,0,0), fwd, Color.red, 0.5f);
        //  RaycastHit hit = new RaycastHit();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            nameText.text = hit.collider.name;
     //       Debug.Log("There is something in front of the object!");
        }
        else
        {
            nameText.text = "";
        }
    }
}
