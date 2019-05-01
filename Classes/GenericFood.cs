using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericFood : MonoBehaviour
{

    protected state.foodType food;
    protected int min = 1, max = 5;
    protected int rating, initRating;
    protected bool isCookingL, isCookingR;

    //Every food in the game can be seasoned & have some in game effect
    //as a result of final outcome
    protected abstract void Seasoning();

    protected abstract void Outcome();

    protected void OnSpawn()
    {
        rating = Mathf.RoundToInt((min + max) / 2f);
        initRating = rating;
        isCookingL = false;
        isCookingR = false;
    }

    //lets the game know the user is currently cooking food 
    //with at least one of the hands
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

    //lets game know user is no longer cooking food
    //with at least one of the hands
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