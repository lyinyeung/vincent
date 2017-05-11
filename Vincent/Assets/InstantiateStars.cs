using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InstantiateStars : MonoBehaviour {

    public TextAsset csvFile; // Reference of catalog CSV file
    public Transform starPrefab; // Reference for generic star prefab
    public Transform mainCamera; // Target for LookAt billboard effect

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter


    // Use this for initialization
    void Start () {


        string[] records = csvFile.text.Split(lineSeperater);
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            int i = 0;
            float x = 0;
            float y = 0;
            float z = 0;
            foreach (string field in fields)
            {
                switch (i)
                {
                    case 0:
                        x = float.Parse(field);
                        break;
                    case 1:
                        y = float.Parse(field);
                        break;
                    case 2:
                        z = float.Parse(field);
                        Instantiate(starPrefab, new Vector3(x, y, z), Quaternion.identity).LookAt(mainCamera);
                        break;
                    default:
                        break;
                }
                i++;
            }
        }

    }
	
    /*
	// Update is called once per frame
	void Update () {
		
	}
    */
}
