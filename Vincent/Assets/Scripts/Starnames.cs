using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Starnames : MonoBehaviour
{
    public Button yourButton;
    public GameObject starnames;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        starnames.SetActive(!starnames.activeSelf);
    }
}