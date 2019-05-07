using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Meat : GenericFood {

	void Start ()
    {
        OnSpawn();
        food = state.foodType.Meat;
    }
	
	void Update ()
    {
        if (isCookingL)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { Seasoning(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { Pouring(); isCookingL = false; }
        }

        if (isCookingR)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { Seasoning(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { Pouring(); isCookingR = false; }
        }
    }

    void Pouring ()
    {
        Debug.Log("pouring stuff into " + this.name);
    }
}