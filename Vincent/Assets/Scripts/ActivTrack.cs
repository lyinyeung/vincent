using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivTrack : MonoBehaviour {

    public Button yourButton;
    public Transform target;
    public GameObject mainCam;

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
      //  mainCam.GetComponent<CamTrackObj>().target = target;
      //  mainCam.GetComponent<CamTrackObj>().enabled = !mainCam.GetComponent<CamTrackObj>().enabled;
        if (!mainCam.GetComponent<CamTrackObj>().enabled)
        {
            mainCam.GetComponent<CamTrackObj>().target = target;
            mainCam.GetComponent<CamTrackObj>().enabled = true;

        } else if (mainCam.GetComponent<CamTrackObj>().target == target)
        {
            mainCam.GetComponent<CamTrackObj>().enabled = false;
        } else
        {
            mainCam.GetComponent<CamTrackObj>().target = target;
        }

    }
    
}
