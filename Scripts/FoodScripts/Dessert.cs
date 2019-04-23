using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dessert : genericFood {

	// Use this for initialization
	void Start ()
    {
        OnSpawn();
        food = state.foodType.Dessert;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isCookingL)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { seasoning(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { throwing(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { shooting(); isCookingL = false; }
        }

        if (isCookingR)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { seasoning(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { throwing(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { shooting(); isCookingR = false; }
        }
    }

    void throwing ()
    {
        //do something
        Debug.Log("throwing stuff into " + this.name);
    }

    void shooting ()
    {
        //do something
        Debug.Log("shooting stuff into " + this.name);
    }

}
