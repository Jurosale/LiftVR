using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour {

    List<GameObject> pocket = new List<GameObject>();

    SDKAdjust handManager;

    public GameObject handLeft, handRight;

    // Use this for initialization
    void Start () {
        //keeps track of the objects in player's hands
        handManager = GameObject.FindGameObjectWithTag("Main").GetComponent<SDKAdjust>();
        handLeft = handManager.lControl.transform.GetChild(0).gameObject;
        handRight = handManager.rControl.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (handLeft == null) handLeft = handManager.lControl.transform.GetChild(0).gameObject;
        if (handRight == null) handRight = handManager.rControl.transform.GetChild(0).gameObject;
    }

    void GrabInPocket (GameObject thisHand) {
        if (pocket.Count > 0) {
            //TODO: instantiate object into player's hand
            //let's game know this object is in player's hand
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

    void PutInPocket(GameObject thisObj)
    {
        //TODO: add tag/layer for these objects
        if (thisObj != null /*&& thisObj.tag or thisObj.layer == [insert desired tag/layer name here]*/)
        {
            pocket.Insert(pocket.Count, thisObj);
            Destroy(thisObj);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grabPointR")
        {
            if (handRight.GetComponent<DeviceInfo>().trigger) {
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
