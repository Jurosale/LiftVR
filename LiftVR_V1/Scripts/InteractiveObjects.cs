using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjects : MonoBehaviour {

    //When the player holds either hand controller on the object and holds trigger, an interaction will occur 
    private Rigidbody rb;
    public GameObject item1;
    public GameObject item2;

    [SerializeField]
    SteamVR_TrackedObject trackedObj;
    [SerializeField]
    SteamVR_TrackedObject trackedObj2;
    private GameObject item1A;
    private GameObject item2A;
    private float item1T;
    private float item2T;
    private GameObject lHold;
    private GameObject rHold;
    float x1;
    float y1;
    float z1;
    float x2;
    float y2;
    float z2;
    private SteamVR_Controller.Device device {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_Controller.Device device2 {
        get {
            return SteamVR_Controller.Input((int)trackedObj2.index);
        }
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        item1 = GameObject.FindGameObjectWithTag("objA");
        x1 = item1.transform.position.x;
        y1 = item1.transform.position.y;
        z1 = item1.transform.position.z;
        item2 = GameObject.FindGameObjectWithTag("objB");
        x2 = item2.transform.position.x;
        y2 = item2.transform.position.y;
        z2 = item2.transform.position.z;
        item1T = 0.0f;
        item2T = 0.0f;
        lHold = null;
        rHold = null;
    }
    
    // Update is called once per frame
    void Update () {
        //if left/right hand on cube object and trigger is pressed, makes cube disappear
        if (((device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj.transform.position, item1.transform.position) < .2) && (rHold == item1 || rHold == null)) || 
            (device2.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj2.transform.position, item1.transform.position) < .2) && (lHold == item1 || lHold == null))
        {
            item1.GetComponent<Rigidbody>().isKinematic = true;
            item1.transform.rotation = Quaternion.identity;
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj.transform.position, item1.transform.position) < .2)
            {
                if (rHold == null || rHold == item1)
                {
                    item1.transform.position = trackedObj.transform.position;
                    item1T = 10.0f;
                    rHold = item1;
                }
                else { rHold = null; }
            }
            else
            {
                if (lHold == null || lHold == item1)
                {
                    item1.transform.position = trackedObj2.transform.position;
                    item1T = 10.0f;
                    lHold = item1;
                }
                else { lHold = null; }
            }
        }
        else {
            //after 5 seconds, cube returns back on the shelf
            item1.GetComponent<Rigidbody>().isKinematic = false;
            item1.transform.rotation = Quaternion.identity;
            item1T -= Time.deltaTime;
            if (item1T <= 0) { item1.transform.position = new Vector3(x1,y1,z1); }
            if (rHold == item1) { rHold = null; }
            if (lHold == item1) { lHold = null; }
        }
        //if left/right hand on sphere object and trigger is pressed, makes sphere disappear
        if (((device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj.transform.position, item2.transform.position) < .2) && (rHold == item2 || rHold == null)) ||
            (device2.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj2.transform.position, item2.transform.position) < .2) && (lHold == item2 || lHold == null))
        {
            item2.GetComponent<Rigidbody>().isKinematic = true;
            item2.transform.rotation = Quaternion.identity;
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj.transform.position, item2.transform.position) < .2)
            {
                if (rHold == item2 || rHold == null)
                {
                    item2.transform.position = trackedObj.transform.position;
                    item2T = 10.0f;
                    rHold = item2;
                }
                else { rHold = null; }
            }
            else
            {
                if (lHold == item2 || lHold == null)
                {
                    item2.transform.position = trackedObj2.transform.position;
                    item2T = 10.0f;
                    lHold = item2;
                }
                else { lHold = null; }
            }
        }
        else
        {
            //after 5 seconds, sphere returns back on the shelf
            item2.GetComponent<Rigidbody>().isKinematic = false;
            item2.transform.rotation = Quaternion.identity;
            item2T -= Time.deltaTime;
            if (item2T <= 0) { item2.transform.position = new Vector3(x2, y2, z2); }
            if (rHold == item2) { rHold = null; }
            if (lHold == item2) { lHold = null; }
        }
    }
}
