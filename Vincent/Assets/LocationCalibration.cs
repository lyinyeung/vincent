using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LocationCalibration : MonoBehaviour {

    public InputField longIn;
    public InputField latIn;
    public Button topoBtn;
    public Button geocBtn;
    public Transform rotation_1;
    public Transform rotation_2;
    public Transform rotation_3;
    public SolarSystemCalculations solarSystem;

    // Use this for initialization
    void Start () {

        Button topobtn = topoBtn.GetComponent<Button>();
        topobtn.onClick.AddListener(performRotations);

        Button geocbtn = geocBtn.GetComponent<Button>();
        geocbtn.onClick.AddListener(resetRotations);

    }

    void performRotations()
    {
        resetRotations();
        Debug.Log(solarSystem.lst *15);
        rotation_2.Rotate(new Vector3(0,((float)solarSystem.lst * 15), 0)); // adjust for local sidereal time
        rotation_3.Rotate(new Vector3((float)Convert.ToDouble(latIn.text) - 90, 0, 0)); // adjust for latitude
    }

    void resetRotations()
    {
        rotation_3.rotation = Quaternion.identity;
        rotation_2.rotation = Quaternion.identity;
        rotation_1.rotation = Quaternion.identity;
        rotation_1.Rotate(0, -89.5f, 0);

    }

}
