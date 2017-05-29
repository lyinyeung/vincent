using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSphereGrid : MonoBehaviour {

    public Transform linePrefab;

    // Use this for initialization
    void Start () {
		for (int i = 0; i < 12; i++)
        {
            Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity).Rotate(new Vector3(0, 0 + (15 * i), 0), Space.Self);
        }
     //   for (int i = 0; i < 3; i++)
   //     {
            var lineH = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            lineH.Rotate(new Vector3(90, 0), Space.Self);

      //  }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
