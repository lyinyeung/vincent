using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMode : MonoBehaviour {

    public Button yourButton;
    public GameObject mainCam;

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }


    void TaskOnClick()
    {
        mainCam.GetComponent<TouchCam>().enabled = true;
        mainCam.GetComponent<GyroCamera>().enabled = false;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
