using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : genericFood {

	// Use this for initialization
	void Start ()
    {
        OnSpawn();
        food = state.foodType.Meat;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isCookingL)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { seasoning(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { pouring(); isCookingL = false; }
        }

        if (isCookingR)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { seasoning(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { pouring(); isCookingR = false; }
        }
    }

    void pouring ()
    {
        Debug.Log("pouring stuff into " + this.name);
    }
}