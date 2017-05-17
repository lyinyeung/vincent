using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObj : MonoBehaviour {

    public Transform target;

    // Use this for initialization
    void Start () {
        this.gameObject.transform.LookAt(target);        
		
	}
	
	
}
