using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseDisplay : MonoBehaviour {

    public Sprite[] phaseSprites;
    public GameObject moon;
    public SolarSystemCalculations solarSystem;
    public double currentPhase;
    public int currentPhaseInt;


    // Use this for initialization
    private void Awake()
    {
        phaseSprites = Resources.LoadAll<Sprite>("moon_0");
    }

    void Start () {
        moon.GetComponent<SpriteRenderer>().sprite = phaseSprites[0];
        currentPhase = solarSystem.moonPhase;

    }
	
	// Update is called once per frame
	void Update () {
        if (solarSystem.moonPhase != currentPhase)
        {
            currentPhase = solarSystem.moonPhase;
            currentPhaseInt = (int) Mathf.Round((float) currentPhase * 10);

            Debug.Log(currentPhaseInt);
            if (currentPhaseInt >= 0)
            {

                moon.GetComponent<SpriteRenderer>().sprite = phaseSprites[10 - currentPhaseInt];
            }
            else
            {

                moon.GetComponent<SpriteRenderer>().sprite = phaseSprites[11 - currentPhaseInt];
            }
            
        }

	}
}
