using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericFood : MonoBehaviour {

    public state.foodType food;
    private static int min = 1, max = 5;
    protected int rating;
    protected bool isCookingL, isCookingR;

    protected void OnSpawn ()
    {
        rating = Mathf.RoundToInt((min + max) / 2);
        isCookingL = false;
        isCookingR = false;
    }

    protected void seasoning ()
    {
        //do something
    }

    protected void Outcome ()
    {
        int initialRating = Mathf.RoundToInt((min + max) / 2);
        
        //if food is worse
        if (rating < initialRating)
        {
            //do something
        }

        //else if food is better
        else if (rating > initialRating)
        {
            //do something
        }

        //else if food is the same
        else
        {
            //do something
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "grabPointR")
        {
            isCookingR = true;
        }

        if (other.gameObject.tag == "grabPointL")
        {
            isCookingL = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "grabPointR")
        {
            isCookingR = false;
        }

        if (other.gameObject.tag == "grabPointL")
        {
            isCookingL = false;
        }
    }
}
