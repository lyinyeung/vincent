using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationDropdown : MonoBehaviour {

    public Dropdown locDropdown;
    public Text defaultTxt;
    public InputField longtxt;
    public InputField lattxt;

	// Use this for initialization
	void Start () {
        defaultTxt.text = "Choose a city";
        locDropdown.onValueChanged.AddListener(delegate { TaskOnChange(); });
	}

    void TaskOnChange()
    {
        int v = locDropdown.value;

        switch (v)
        {
            case 0:
                lattxt.text = "39.9042";
                longtxt.text = "116.4074";
                break;
            case 1:
                lattxt.text = "52.5200";
                longtxt.text = "13.4050";
                break;
            case 2:
                lattxt.text = "30.0444";
                longtxt.text = "31.2357";
                break;
            case 3:
                lattxt.text = "22.3964";
                longtxt.text = "114.1095";
                break;
            case 4:
                lattxt.text = "51.5074";
                longtxt.text = "0.1278";
                break;
            case 5:
                lattxt.text = "45.4654";
                longtxt.text = "9.1859";
                break;
            case 6:
                lattxt.text = "55.7558";
                longtxt.text = "37.6173";
                break;
            case 7:
                lattxt.text = "28.6139";
                longtxt.text = "77.2090";
                break;
            case 8:
                lattxt.text = "40.7128";
                longtxt.text = "74.0059";
                break;
            case 9:
                lattxt.text = "48.8566";
                longtxt.text = "2.3522";
                break;
            case 10:
                lattxt.text = "35.6895";
                longtxt.text = "139.6917";
                break;
            default: break;

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
