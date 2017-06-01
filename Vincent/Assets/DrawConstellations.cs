using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConstellations : MonoBehaviour {

    public TextAsset csvFile; // Reference of catalog CSV file
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    public Transform constParent;  // Parent object of lines

    // Use this for initialization
    void Start()
    {

        string[] records = csvFile.text.Split(lineSeperater);
        Vector3 lineStart = new Vector3(0, 0, 0);
        Vector3 lineEnd = new Vector3(0, 0, 0);
       // bool draw = false;
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            int i = 0;
            float x = 0;
            float y = 0;
            float z = 0;

            foreach (string field in fields)
            {
                switch (i)
                {
                    case 0:                        
                        x = float.Parse(field);                       
                        break;
                    case 1:                 
                        y = float.Parse(field);                        
                        break;
                    case 2:
                        lineStart = lineEnd;
                        z = float.Parse(field);
                        lineEnd = new Vector3(-z, y, x);
                        if (lineStart != new Vector3(0,0,0) && lineEnd != new Vector3(0, 0, 0))
                        {
                            var l = DrawLine(lineStart, lineEnd, Color.white);
                            l.transform.parent = constParent;
                        }
        
                        break;
                    default:break;
                }
                i++;
            }
        }
    }

    GameObject DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Standard"));
     //   lr.material = new Material(Shader.Find(""));
        //  lr.SetColors(color, color);
        lr.startColor = color;
        lr.endColor = color;
        // lr.SetWidth(0.1f, 0.1f);
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.alignment = LineAlignment.View;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        return myLine;
   //     GameObject.Destroy(myLine, duration);
    }
}
