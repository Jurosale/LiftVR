//follows same format as "Pocket" script
//spawns with each patron

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronPocket : MonoBehaviour {

    List<GameObject> patronPocket = new List<GameObject>();

    Pocket avatarPocket;

    //needs to be set in main patron script
    public bool canPickPocket;

    // Use this for initialization
    void Start()
    {
        //keeps track of the objects in player's hands
        avatarPocket = GameObject.Find("Hip").GetComponent<Pocket>();
    }

    void GrabInPatronPocket(GameObject thisHand)
    {
        if (patronPocket.Count > 0)
        {
            // TODO: instantiate object into patron's hand
            //let's game know this object is in player's hand
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

    void PutInPatronPocket(GameObject thisObj)
    {
        //TODO: create the tag/layer for these objects 
        if (thisObj != null /*&& thisObj.tag or thisObj.layer == [insert desired tag/layer name here]*/)
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