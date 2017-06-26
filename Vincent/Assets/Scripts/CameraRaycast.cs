using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour {

    public Text nameText;
    public Button learnBtn;
    public string currentTarget = "";


	// Use this for initialization
	void Start () {
        Button btn = learnBtn.GetComponent<Button>();
        btn.onClick.AddListener(openURL);

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * 550;
        Debug.DrawRay(new Vector3(0,0,0), fwd, Color.red, 0.5f);
        //  RaycastHit hit = new RaycastHit();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
           
            nameText.text = hit.collider.name;
            currentTarget = hit.collider.name;
            learnBtn.gameObject.SetActive(true);
     //       Debug.Log("There is something in front of the object!");
        }
        else
        {
            learnBtn.gameObject.SetActive(false);
            nameText.text = "";
        }
    }

    void openURL()
    {
        string url = "https://en.wikipedia.org/wiki/" + currentTarget;
        Application.OpenURL(url);
        //starnames.SetActive(false);
    }
}
