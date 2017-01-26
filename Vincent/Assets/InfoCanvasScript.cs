using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvasScript : MonoBehaviour {
    

    // Use this for initialization
    void Start () {

        gameObject.SetActive(false);
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.SetActive(false);
        }
    }
}
