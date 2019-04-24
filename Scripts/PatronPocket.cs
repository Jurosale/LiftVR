//follows same format as "Pocket" script
//spawns with each patron

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronPocket : MonoBehaviour
{

    private List<GameObject> patronPocket = new List<GameObject>();

    Pocket avatarPocket;

    //implement a setter in main patron script to change this bool
    private bool canPickPocket;

    // Use this for initialization
    void Start()
    {
        //keeps track of the objects in player's hands
        avatarPocket = GameObject.Find("Hip").GetComponent<Pocket>();
    }

    //if theres an object in the patron's inventory system,
    //will "pull" it out of pocket
    void GrabInPatronPocket(GameObject thisHand)
    {
        if (patronPocket.Count > 0)
        {
            Instantiate(patronPocket[0], thisHand.transform.localPosition, Quaternion.identity);

            //checks to see if it player's left or right hand
            if (thisHand.tag == "grabPointR")
            {
                avatarPocket.handRight = patronPocket[0];
            }

            else
            {
                avatarPocket.handLeft = patronPocket[0];
            }

            patronPocket.RemoveAt(0);
        }

    }

    //if player is holding object and this object can be put
    //into patron's pocket,then it will be "pushed in pocket"
    //and be stored in the patron's inventory system
    void PutInPatronPocket(GameObject thisObj)
    {
        if (thisObj != null && thisObj.layer == 1)
        {
            patronPocket.Insert(patronPocket.Count, thisObj);
            Destroy(thisObj);
        }
    }

    //call this in the main patron script and insert desired item(s)
    void SpawnObjInPatronPocket(GameObject thisObj)
    {
        patronPocket.Insert(patronPocket.Count, thisObj);
    }

    //checks and determines whether player is trying to
    //grab an object out of patron's pocket or put one
    //in and with which hand specifically
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grabPointR")
        {
            if (avatarPocket.handRight.GetComponent<DeviceInfo>().trigger && canPickPocket)
            {
                GrabInPatronPocket(other.gameObject);
            }

            else if (avatarPocket.handRight.GetComponent<DeviceInfo>().triggerRelease && avatarPocket.handRight != null)
            {
                PutInPatronPocket(avatarPocket.handRight);
            }
        }

        if (other.gameObject.tag == "grabPointL")
        {
            if (avatarPocket.handLeft.GetComponent<DeviceInfo>().trigger && canPickPocket)
            {
                GrabInPatronPocket(other.gameObject);
            }

            else if (avatarPocket.handLeft.GetComponent<DeviceInfo>().triggerRelease && avatarPocket.handLeft != null)
            {
                PutInPatronPocket(avatarPocket.handLeft);
            }
        }
    }
}