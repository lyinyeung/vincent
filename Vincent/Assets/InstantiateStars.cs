using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InstantiateStars : MonoBehaviour {

    public TextAsset csvFile; // Reference of catalog CSV file
    public Transform starPrefab; // Reference for generic star prefab
    public Camera mainCamera; // Target for LookAt billboard effect

    public GameObject fov80;
    public GameObject fov65;
    public GameObject fov60;
    public GameObject fov45;
    public GameObject fov30;
    public GameObject fov25;
    public GameObject fov20;



    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter


    // Use this for initialization
    void Start () {
        fov80.SetActive(false);
        fov65.SetActive(false);
        fov60.SetActive(false);
        fov45.SetActive(false);
        fov30.SetActive(false);
        fov25.SetActive(false);
        fov20.SetActive(false);


        string[] records = csvFile.text.Split(lineSeperater);
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            int i = 0;
            float x = 0;
            float y = 0;
            float z = 0;
            float delta = 1;
            float refSize = 3;
            Color c = new Color(0,0,0);
            foreach (string field in fields)
            {
                switch (i)
                {
                    case 0:
                        z = float.Parse(field);
                        break;
                    case 1:
                        y = float.Parse(field);
                        break;
                    case 2:
                        x = float.Parse(field);            
                        break;
                    case 3:
                        c = Bv2rgb(float.Parse(field));
                        break;
                    case 4:
                        float mag = float.Parse(field);
                        delta = Mathf.Pow(2.512F,-mag);
                        
                        var blur = Instantiate(starPrefab, new Vector3(x, y, z), Quaternion.identity);
                        var star = Instantiate(starPrefab, new Vector3(x, y, z), Quaternion.identity);
                        star.LookAt(mainCamera.transform);
                        blur.LookAt(mainCamera.transform);
                        star.localScale += new Vector3(delta*refSize, delta * refSize);
                        blur.localScale += new Vector3(delta * refSize, delta * refSize);
                        blur.localScale += new Vector3(1.5F, 1.5F);
                        star.GetComponent<Renderer>().material.color = c;
                        c.a = 0.3F;
                        blur.GetComponent<Renderer>().material.color = c;

                        if (mag<=5.0)
                        {
                            blur.transform.parent = fov80.transform;
                            star.transform.parent = fov80.transform;
                        }
                        else if (mag<=5.5 && mag > 5.0)
                        {
                            blur.transform.parent = fov65.transform;
                            star.transform.parent = fov65.transform;
                        }
                        else if (mag<=5.75 && mag > 5.5)
                        {
                            blur.transform.parent = fov60.transform;
                            star.transform.parent = fov60.transform;
                        }
                        else if (mag<=6.0 && mag > 5.75)
                        {
                            blur.transform.parent = fov45.transform;
                            star.transform.parent = fov45.transform;
                        }
                        else if (mag<=6.25 && mag > 6.0)
                        {
                            blur.transform.parent = fov30.transform;
                            star.transform.parent = fov30.transform;
                        }
                        else if (mag<=6.5 && mag > 6.25)
                        {
                            blur.transform.parent = fov25.transform;
                            star.transform.parent = fov25.transform;
                        }
                        else
                        {
                            blur.transform.parent = fov20.transform;
                            star.transform.parent = fov20.transform;
                        }

                        break;
                    default:
                        break;
                }
                i++;
            }
        }


    }

    static Color Bv2rgb(double bv)    // Conversion from B-V color index to RGB, RGB <0,1> <- BV <-0.4,+2.0> [-]
    {
        double t;
        double r = 0.0;
        double g = 0.0;
        double b = 0.0;
        if (bv < -0.4) bv = -0.4;
        if (bv > 2.0) bv = 2.0;
        if ((bv >= -0.40) && (bv < 0.00))
        {
            t = (bv + 0.40) / (0.00 + 0.40);
            r = 0.61 + (0.11 * t) + (0.1 * t * t);
        }
        else if ((bv >= 0.00) && (bv < 0.40))
        {
            t = (bv - 0.00) / (0.40 - 0.00);
            r = 0.83 + (0.17 * t);
        }
        else if ((bv >= 0.40) && (bv < 2.10))
        {
            t = (bv - 0.40) / (2.10 - 0.40);
            r = 1.00;
        }
        if ((bv >= -0.40) && (bv < 0.00))
        {
            t = (bv + 0.40) / (0.00 + 0.40);
            g = 0.70 + (0.07 * t) + (0.1 * t * t);
        }
        else if ((bv >= 0.00) && (bv < 0.40))
        {
            t = (bv - 0.00) / (0.40 - 0.00);
            g = 0.87 + (0.11 * t);
        }
        else if ((bv >= 0.40) && (bv < 1.60))
        {
            t = (bv - 0.40) / (1.60 - 0.40);
            g = 0.98 - (0.16 * t);
        }
        else if ((bv >= 1.60) && (bv < 2.00))
        {
            t = (bv - 1.60) / (2.00 - 1.60);
            g = 0.82 - (0.5 * t * t);
        }
        if ((bv >= -0.40) && (bv < 0.40))
        {
            t = (bv + 0.40) / (0.40 + 0.40);
            b = 1.00;
        }
        else if ((bv >= 0.40) && (bv < 1.50))
        {
            t = (bv - 0.40) / (1.50 - 0.40);
            b = 1.00 - (0.47 * t) + (0.1 * t * t);
        }
        else if ((bv >= 1.50) && (bv < 1.94))
        {
            t = (bv - 1.50) / (1.94 - 1.50);
            b = 0.63 - (0.6 * t * t);
        }

        return new Color((float)r, (float)g, (float)b, 0.7F);
    }

    
	// Update is called once per frame
	void Update () {
        if (mainCamera.fieldOfView <= 80)
        {
            fov80.SetActive(true);
        }

        if (mainCamera.fieldOfView <= 65)
        {
            fov65.SetActive(true);
        }
        else
        {
            fov65.SetActive(false);
        }

        if (mainCamera.fieldOfView <= 60)
        {
            fov60.SetActive(true);
        }
        else
        {
            fov60.SetActive(false);
        }

        if (mainCamera.fieldOfView <= 45)
        {
            fov45.SetActive(true);
        }
        else
        {
            fov45.SetActive(false);
        }

        if (mainCamera.fieldOfView <= 30)
        {
            fov30.SetActive(true);
        }
        else
        {
            fov30.SetActive(false);
        }

        if (mainCamera.fieldOfView <= 25)
        {
            fov25.SetActive(true);
        }
        else
        {
            fov25.SetActive(false);
        }

        if (mainCamera.fieldOfView <= 20)
        {
            fov20.SetActive(true);
        }
        else
        {
            fov20.SetActive(false);
        }
    }
    
}
