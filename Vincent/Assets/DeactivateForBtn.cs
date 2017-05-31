using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateForBtn : MonoBehaviour {


    public Button yourButton;
    public GameObject starnames;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        starnames.SetActive(false);
    }
}