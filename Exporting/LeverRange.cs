using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRange : MonoBehaviour {

    public static bool inRange;
    private GameObject camRig;
	public static bool nearLever;

    [Header("Initial Grab")]
    [Range(0, 10)]
    public float initialGrabStrength;
    [Range(0, 1)]
    public float initialGrabLength;

    private int handsColliding = 0;

    // Use this for initialization
    void Start () {
        inRange = false;
        camRig = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update () {
		if (camRig == null)
			camRig = GameObject.FindGameObjectWithTag("Player");
		

        if(inRange == false) {
            GetComponent<LeverRotation>().grabbed = false;
        }
	}

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "grabPoint") {
            inRange = true;
            handsColliding += 1;
        }
    }
 
    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "grabPoint")
        {
            handsColliding -= 1;
            if (handsColliding <= 0) {
                inRange = false;
            }
        }
    }
 

    public void attemptGrab(GameObject hand, bool doorOpen, float timer) {
        if (inRange)
        {
			nearLever = true;
			//camRig.GetComponent<DisableGesture>().turnOff();

            if (!doorOpen && timer <= 0.0f) {
                GetComponent<LeverRotation>().startGrab(hand);
            } else {
                GetComponent<LeverRotation>().jiggleResponse();
            }

            var whichHand = "both";
            //Trigger Haptic pulse
            if(hand.tag == "leftControl") {
                whichHand = "left";
            }
            else if(hand.tag == "rightControl") {
                whichHand = "right";
            }
            Haptic.rumbleController(initialGrabLength, initialGrabStrength, whichHand);

        } else {
			nearLever = false;
		//	if (!camRig.GetComponent<DisableGesture>().isComponentEnabled()) {
			//	camRig.GetComponent<DisableGesture>().turnOn();
          //  }
        }
    }

    public void attemptRelease()  {
        GetComponent<LeverRotation>().grabbed = false;
    }
}
