using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSphereGrid : MonoBehaviour {

    public Transform linePrefab;
    public Transform parent; // Top level parent 

    // Use this for initialization
    void Start () {
		for (int i = 0; i < 12; i++)
        {
            var lineV = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            lineV.Rotate(new Vector3(0, 0 + (15 * i), 0), Space.Self);
            lineV.parent = parent;

        }
        //   for (int i = 0; i < 3; i++)
        //     {

        var lineH = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        lineH.Rotate(new Vector3(90, 0), Space.Self);
        lineH.parent = parent;

      //  }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
