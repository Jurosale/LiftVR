using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChainController : MonoBehaviour {
    /// <summary>
    /// Dealing with the chain to control the openign of the door
    /// 
    /// Left rope you pull down to open,
    /// right rope you pull down to close
    /// 
    /// if left rope is pulled and door is closed open
    ///     else dont 
    /// 
    /// </summary>
    /// 

    public GameObject openDoorRope;    //Left
    public GameObject closeDoorRope;   //Right

    public ElevatorGlobals eg;




	// Use this for initialization
	void Start () {
		// get state of the door
        
	}
	
	// Update is called once per frame
	void Update () {
		// if rope is grabbed and door is open
        if (openDoorRope.GetComponent<VRTK_InteractableObject>().IsGrabbed() && !eg.doorOpen) {
            // OPEN DOOR HERE
            print("we want to be opening the door here");
            // start rope movement anim here?
            openDoorRope.GetComponent<Animator>().SetTrigger("Down");
            closeDoorRope.GetComponent<Animator>().SetTrigger("Rise");
        }
        else if (openDoorRope.GetComponent<VRTK_InteractableObject>().IsGrabbed() && eg.doorOpen) {
            // DOOR IS ALREADY OPEN
            // DO NOTHIGN?
            // maybe do that similar thing to the lever and make it twitch or w/e
        }

        if (closeDoorRope.GetComponent<VRTK_InteractableObject>().IsGrabbed() && eg.doorOpen) {
            // CLOSE DOOR HERE
            print("we want to be closing the door here");
            // start rope movement anim here?
            openDoorRope.GetComponent<Animator>().SetTrigger("Rise");
            closeDoorRope.GetComponent<Animator>().SetTrigger("Down");
        } else if (closeDoorRope.GetComponent<VRTK_InteractableObject>().IsGrabbed() && !eg.doorOpen) {
            // DOOR IS ALREADY CLOSED
            // DO NOTHIGN
            // maybe do that similar thing to the lever and make it twitch or w/e
        }


    }
}
