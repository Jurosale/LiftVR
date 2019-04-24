using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Soup : genericFood {

	// Use this for initialization
	void Start ()
    {
        OnSpawn();
        food = state.foodType.Soup;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isCookingL)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { seasoning(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { stirring(); isCookingL = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { throwing(); isCookingL = false; }
        }

        if (isCookingR)
        {
            //used for testing purposes, will delete once gestures have been implemented
            if (Input.GetKeyDown(KeyCode.Z)) { seasoning(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.C)) { stirring(); isCookingR = false; }
            else if (Input.GetKeyDown(KeyCode.X)) { throwing(); isCookingR = false; }
        }
    }

    void stirring ()
    {
        Debug.Log("stirring stuff into " + this.name);
    }

    void throwing ()
    {
        Debug.Log("throwing stuff into " + this.name);
    }
}