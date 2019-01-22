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
        //handInfo = GameObject.Find("PlayerManager").GetComponent<Hands>();
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
        print("uhhhhhhhhhhhhhhh 0000000000 ");
        if (pocket.Count > 0) {
           // Instantiate(pocket[0], thisHand.transform.position, Quaternion.identity);
            print("uhhhhhhhhhhhhhhh 11111111");
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
        print("uhhhhhhhhhhhhhhh 222222");

        if (thisObj != null /*&& thisObj.tag or thisObj.layer == [insert desired tag/layer name here]*/)
        {
            pocket.Insert(pocket.Count, thisObj);
           // Destroy(thisObj);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grabPointR")
        {
            if (handRight.GetComponent<DeviceInfo>().trigger) {
                print("right hand in and then it triggered");
                GrabInPocket(other.gameObject);
            }

            else if (handRight.GetComponent<DeviceInfo>().triggerRelease)
            {
                print("right hand in and then it released");

                if (handRight != null)
                {
                    PutInPocket(handRight);
                    //handRight = null;
                }
            }
        }

        if (other.gameObject.tag == "grabPointL")
        {
           // if (VRInput.getLeftTriggerDown())
            if (handLeft.GetComponent<DeviceInfo>().trigger)
            {
                print("left hand in and then it triggered");

                GrabInPocket(other.gameObject);
            }

           // else if (VRInput.getLeftTriggerRelease())
            else if (handLeft.GetComponent<DeviceInfo>().triggerRelease)
            {
                print("left hand in and then it released");

                if (handLeft != null)
                {
                    PutInPocket(handLeft);
                    //handLeft = null;
                }
            }
        }
    }
}
