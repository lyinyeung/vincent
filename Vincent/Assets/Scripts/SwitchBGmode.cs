using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchBGmode : MonoBehaviour
{
    public Button yourButton;
    public GameObject starmapSphere;
    public GameObject constellationSphere;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        starmapSphere.SetActive(!starmapSphere.activeSelf);
        constellationSphere.SetActive(!constellationSphere.activeSelf);
    }
}