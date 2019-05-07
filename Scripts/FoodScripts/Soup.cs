using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Soup : GenericFood {

	void Start ()
    {
        OnSpawn();
        food = state.foodType.Soup;
    }
	
	void Update ()
    {
        if (isCookingL)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { Seasoning(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { Stirring(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { Throwing(); isCookingL = false; }
        }

        if (isCookingR)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { Seasoning(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { Stirring(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { Throwing(); isCookingR = false; }
        }
    }

    void Stirring ()
    {
        Debug.Log("stirring stuff into " + this.name);
    }

    void Throwing ()
    {
        Debug.Log("throwing stuff into " + this.name);
    }
}