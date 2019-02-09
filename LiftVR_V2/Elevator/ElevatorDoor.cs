using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ElevatorDoor : MonoBehaviour {

    public GameObject elevatorDoor; // animation
    public GameObject liftableDoor; // interacted door
    public GameObject animatedDoor;
    public GameObject door_lever;
    float leverVal;

    float doorClosedY = -1.202f;
    float doorOpenY = 2.895f;

    //Resting Positions
    Vector3 doorOpenResting; // 4.612, 3, 0
    Vector3 doorClosedResting; // 4.612, -1.161, 0

    public float doorBeginLerpUpY, doorBeginLerpDownY;

    //current positions
    float doorY;

  //  public float midPoint = 2.4f;
  //  public float highPoint = 3.2f;

    bool opening;
    bool closing;

   // float topState = 3f; // start
   // float closedState = 0f; //closed state

	// Use this for initialization
	void Start () {
        doorOpenResting = liftableDoor.transform.position;
        leverVal = ControlReactor.leverValue;
    }

    // Update is called once per frame
    void Update() {
        leverVal = ControlReactor.leverValue;

        // get the lever value
        // if the lever value is changing, move the door with it
        // doorClosed.Y == leverVal of 100, doorOpen.y == leverVal of 0

        doorY = Mathf.Lerp(doorClosedY, doorOpenY, Mathf.InverseLerp(0, 100, leverVal));
        liftableDoor.transform.localPosition = new Vector3(0.9217682f, doorY ,0.3204571f);

        if (doorY > 2) {
            OpenDoor();
        } else if (doorY < -1) {
            CloseDoor();
        }

        //  currDoorPos = liftableDoor.transform.position;
        //  currRopePos = rope.transform.position;

        //    if (r.attemptOpen && !r.attemptClose)  {
        //we are trying to open this shit
        //    opening = true;   
        // Move the door according to the current y position of the controller
        //  liftableDoor.transform.position = new Vector3(currDoorPos.x, (liftableDoor.transform.position.y - ropeHandle.transform.position.y) + r.grabbingHand.transform.position.y, currDoorPos.z);
        // otherwise Y pos of door and pulley will lerp auto


        // Once the door reaches a certain height
        // it goes all the way up automatically

        //        var whichHand = "both";
        //        //Trigger Haptic pulse
        //        if (r.grabbingHand.tag == "leftControl")
        //        {
        //            whichHand = "left";
        //        }
        //        else if (r.grabbingHand.tag == "rightControl")
        //        {
        //            whichHand = "right";
        //        }
        //       // var strength = (Mathf.Abs(door.transform.position.y - previousHeight) / (highPoint - initY)) * holdingVibrationMax;
        //       // Haptic.rumbleController(0.1f, strength, whichHand);

        //    }  else if (!r.attemptOpen && r.attemptClose) {

        //        if (liftableDoor.transform.position.y < midPoint && opening)
        //        {
        //            // while the door position is greater than the y position
        //            liftableDoor.transform.position = Vector3.MoveTowards(liftableDoor.transform.position, new Vector3(currDoorPos.x, 1.1f, currDoorPos.z), 2f * Time.deltaTime);
        //        }
        //    }
        //}
    }

    public void OpenDoor()
    {
        GetComponent<ElevatorGlobals>().doorOpen = true;
        // elevatorDoor.SetActive(false);
        // set open the moving doors
        animatedDoor.GetComponent<Animator>().SetBool("open", true);
        animatedDoor.GetComponent<Animator>().SetBool("closed", false);


        closing = false;
        opening = true;
        print("door open");
       // liftableDoor.transform.position = doorOpenResting;
        // LERP UP FROM DEFAULT POS HERE
    }

    public void CloseDoor()
    {
        GetComponent<ElevatorGlobals>().doorOpen = false;
        // elevatorDoor.SetActive(true);
        animatedDoor.GetComponent<Animator>().SetBool("closed", true);
        animatedDoor.GetComponent<Animator>().SetBool("open", false);

        closing = true;
        opening = false;
        print("door closed");

        // liftableDoor.transform.position = doorClosedResting;

        // LERP DOWN FROM DEFAULT POS HERE

    }


    //used in LeverGrab script to stop the lever from moving if door is open
    public bool doorStatus()
    {
        return opening;
    }
}
