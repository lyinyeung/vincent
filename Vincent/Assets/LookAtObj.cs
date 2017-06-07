using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObj : MonoBehaviour {

    public Transform target;

    // Use this for initialization
    void Update () {
        this.transform.LookAt(target);        
		
	}
	
	
}
