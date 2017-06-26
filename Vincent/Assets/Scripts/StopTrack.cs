using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopTrack : MonoBehaviour {

    public Button yourButton;
    public GameObject mainCam;

    // Use this for initialization
    void Start () {

        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        mainCam.GetComponent<CamTrackObj>().enabled = !mainCam.GetComponent<CamTrackObj>().enabled;

    }
}
