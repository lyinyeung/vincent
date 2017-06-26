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
    public DrawConstellations drawConst;
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
        rotation_3.rotation = Quaternion.identity;
        rotation_2.rotation = Quaternion.identity;
        rotation_1.rotation = Quaternion.identity;
        rotation_1.Rotate(0, -89.5f, 0);

        rotation_2.Rotate(new Vector3(0f,((float)solarSystem.lst * 15), 0f)); // adjust for local sidereal time
        rotation_3.Rotate(new Vector3((float)Convert.ToDouble(latIn.text) - 90, 0f, 0f)); // adjust for latitude
        Quaternion rot1 = Quaternion.Euler(0f, ((float)solarSystem.lst * 15), 0f);
        Quaternion rot2 = Quaternion.Euler((float)Convert.ToDouble(latIn.text) - 90, 0f, 0f);
        Quaternion rot3 = Quaternion.Euler(0f, 0f, 0f);
        drawConst.InstantiateConstellations(rot1, rot2, rot3);
    }

    void resetRotations()
    {
        rotation_3.rotation = Quaternion.identity;
        rotation_2.rotation = Quaternion.identity;
        rotation_1.rotation = Quaternion.identity;
        rotation_1.Rotate(0, -89.5f, 0);

        drawConst.ResetConsts();
    }
}
