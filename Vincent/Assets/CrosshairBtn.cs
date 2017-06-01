using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBtn : MonoBehaviour {

    public Button yourButton;
    public GameObject mainCam;
    public GameObject learnBtn;
    public Text nameTxt;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        mainCam.GetComponent<CameraRaycast>().enabled = !mainCam.GetComponent<CameraRaycast>().enabled;
        nameTxt.text = "";
        learnBtn.SetActive(false);
    }
}
