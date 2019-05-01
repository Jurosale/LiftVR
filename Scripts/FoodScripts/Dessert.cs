using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dessert : GenericFood {

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
            if (Input.GetKeyDown(KeyCode.Z)) { Seasoning(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { Throwing(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { Shooting(); isCookingL = false; }
        }

        if (isCookingR)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { Seasoning(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { Throwing(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { Shooting(); isCookingR = false; }
        }
    }

    void Throwing ()
    {
        Debug.Log("throwing stuff into " + this.name);
    }

    void Shooting ()
    {
        Debug.Log("shooting stuff into " + this.name);
    }
}