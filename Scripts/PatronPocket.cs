//Pocket script for patrons
//spawns with each patron

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PatronPocket : Pocket
{

    //implement a setter in main patron script to change this bool
    private bool canPickPocket;

    void Start()
    {
        Setup();
        StartCoroutine("CheckForNull");
    }
    
    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grabPointR" && canPickPocket)
        {
            if (handRight.GetComponent<DeviceInfo>().trigger)
            {
                GrabInPocket(other.gameObject);
            }

            else if (handRight.GetComponent<DeviceInfo>().triggerRelease && handRight != null)
            {
                PutInPocket(handRight);
            }
        }

        if (other.gameObject.tag == "grabPointL" && canPickPocket)
        {
            if (handLeft.GetComponent<DeviceInfo>().trigger)
            {
                GrabInPocket(other.gameObject);
            }

            else if (handLeft.GetComponent<DeviceInfo>().triggerRelease && handLeft != null)
            {
                PutInPocket(handLeft);
            }
        }
    }

}