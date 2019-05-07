using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Pocket : MonoBehaviour
{

    protected List<GameObject> pocket = new List<GameObject>();

    protected SDKAdjust handManager;

    protected GameObject handLeft, handRight;
    
    void Start()
    {
        Setup();
        StartCoroutine("CheckForNull");
    }

    //keeps track of the objects in player's hands
    protected void Setup()
    {
        handManager = GameObject.FindGameObjectWithTag("Main").GetComponent<SDKAdjust>();
        handLeft = handManager.lControl.transform.GetChild(0).gameObject;
        handRight = handManager.rControl.transform.GetChild(0).gameObject;
    }

    // checks every second to see if either hand is null and corrects itself if so
    IEnumerator CheckForNull()
    {
        while (true)
        {
            if (handLeft == null) handLeft = handManager.lControl.transform.GetChild(0).gameObject;
            if (handRight == null) handRight = handManager.rControl.transform.GetChild(0).gameObject;

            yield return new WaitForSeconds(1.0f);
        }
    }

    //if theres an object in the inventory system,
    //will "pull" it out of pocket
    protected void GrabInPocket(GameObject thisHand)
    {
        if (pocket.Count > 0)
        {
            Instantiate(pocket[0], thisHand.transform.localPosition, Quaternion.identity);

            //checks to see if its the right or left hand
            if (thisHand.tag == "grabPointR")
            {
                handRight = pocket[0];
            }

            else
            {
                handLeft = pocket[0];
            }

            pocket.RemoveAt(0);
        }

    }

    //if player is holding object and this object can be put
    //into a pocket,then it will be "pushed in pocket"
    //and be stored in the inventory system
    protected void PutInPocket(GameObject thisObj)
    {
        if (thisObj != null && thisObj.layer == 1)
        {
            pocket.Insert(pocket.Count, thisObj);
            Destroy(thisObj);
        }
    }

    //checks and determines whether player is trying to
    //grab an object out of pocket or put one in and
    //with which hand specifically
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grabPointR")
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

        if (other.gameObject.tag == "grabPointL")
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