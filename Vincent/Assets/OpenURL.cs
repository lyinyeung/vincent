using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenURL : MonoBehaviour
{
    public Button yourButton;
    public string url;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Application.OpenURL(url);
    }
}