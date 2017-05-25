using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSize : MonoBehaviour {

    public Transform t;
    public int dimensions = 1000;

	// Use this for initialization
	void Start () {
        t.localScale = new Vector3(dimensions, dimensions);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
