using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using VRTK;
using VRTK.Examples;

public class RopeScript : MonoBehaviour {

    ObiRopeCursor cursor;
    float minLength = 0.5f;
    float maxRopeHeight; // when rope is here we know door is closed (bc high)
    float minRopeHeight = 2.5f; //when rope is here then we know door is open (bc low)

    //public GameObject rope;
   // public GameObject ropeHandle;
    public GameObject liftableDoor;
    public GameObject grabbingHand = null;

    public GameObject slider;
    VRTK_Slider vrsl;

    ElevatorGlobals eGlobal;


    float pullForce;

    public GameObject lControl;
    public GameObject rControl;

    public bool attemptOpen = false;
    public bool attemptClose = false;

    Vector3 closedDoor = new Vector3(0.9217682f, -1.200081f, 0.3204571f); // orig position (closed)
    Vector3 openDoor = new Vector3(0.9217682f, 2.93f, 0.3204571f); // 

    Vector3 rvel;
    Vector3 lvel;
    Vector3 vel;
    ConfigurableJoint sliderJoint;

    bool doorOpen;
    bool handleGrabbed;

    GameObject playerObject;
    PlayMakerFSM playerFSM;

  
        // Use this for initialization
        void Start() {
      //  cursor = rope.GetComponent<ObiRopeCursor>();
        doorOpen = GetComponent<ElevatorGlobals>().doorOpen;
        handleGrabbed = slider.GetComponent<VRTK_InteractableObject>().IsGrabbed();
        vrsl = slider.GetComponent<VRTK_Slider>();
        eGlobal = GetComponent<ElevatorGlobals>();


        playerObject = GameObject.FindGameObjectWithTag("PlayerManager");
        playerFSM = playerObject.GetComponent<PlayMakerFSM>();

    }

    // Update is called once per frame
    void Update() {
        if (sliderJoint == null) sliderJoint = slider.GetComponent<ConfigurableJoint>();

        handleGrabbed = slider.GetComponent<VRTK_InteractableObject>().IsGrabbed();
        // print("is handle grabbed? " + handleGrabbed + " grabbed by: " + grabbingHand);

        // get velocity of controller grabbing
        // if (slider.GetComponent<VRTK_InteractableObject>().GetGrabbingObject() == lControl) {
        //    grabbingHand = lControl;
        //    vel = lvel;
        //} else if (slider.GetComponent<VRTK_InteractableObject>().GetGrabbingObject() == rControl) {
        //    grabbingHand = rControl;
        // vel = rvel;
        //}
       // print(sliderJoint.targetPosition);

        doorOpen = false;
        // if door is closed and the handle is grasped
        if (!doorOpen && handleGrabbed) {
            // attemptOpen = true;

           // print(vrsl.GetValue());
            // attemptClose = false;
           // print("door open and handle grabbed");

            if (vrsl.GetValue() == 5) {
                // in the middle

            } else if (vrsl.GetValue() > 5) {
                // above
              //  print("lifting");
              //  if (vrsl.GetValue() == 10)
               // {
                    // go back to 5
                    // at the top and door should open
                   // print("OPEning");

                    attemptClose = false;
                    attemptOpen = true;
             //   }
            } else if (vrsl.GetValue() < 5) {
                // below
            //    print("closing");
              //  if (vrsl.GetValue() == 0)
              //  {
                    // go back to 5
                    // at the bottom and door should close
                  //  print("CLSOe");
                    attemptClose = true;
                    attemptOpen = false;

             //   }
            }

            //// once it gets to height where we want it
            //if (cursor.rope.RestLength >= minRopeHeight) {
            //    // stop moving, open door?
            //    print("the cursor rope length is this " + cursor.rope.RestLength + " and min rope is " + minRopeHeight);
            //    print("too long! stopping!");
            //} else {
            //    cursor.ChangeLength(cursor.rope.RestLength + getVelocity(grabbingHand).magnitude * Time.deltaTime);
            //}

            //// door changes height as well ( + y) 
            //print(cursor.rope.RestLength);
          //  liftableDoor.transform.position = Vector3.MoveTowards(liftableDoor.transform.position,
              //  openDoor,
             //   Time.deltaTime);
            // door.transform.position = new Vector3(initX, (door.transform.position.y - hold.transform.position.y) + grabbingHand.transform.position.y, initZ);

        } else if (doorOpen && handleGrabbed) {
            // this is the one touch
            // doorGo up
           // attemptOpen = false;
           // attemptClose = true;
        }
        if (attemptOpen)
        {
            elevatortrigger(openDoor);

        }
        else if (attemptClose) { 

            elevatortrigger(closedDoor);

            }



            // else if door is open and handle is grasped


        }

    void elevatortrigger(Vector3 dir) {
        liftableDoor.transform.localPosition = Vector3.MoveTowards(liftableDoor.transform.localPosition,
                        dir,
                        Time.deltaTime);

        if(liftableDoor.transform.localPosition == dir) 
        {
            print("done w its shit!!!!!!");
            if (dir == closedDoor) {
                eGlobal.GetComponent<ElevatorGlobals>().doorOpen = false;
                playerFSM.SendEvent("DoorClose");
            } else if (dir == openDoor) {
                eGlobal.GetComponent<ElevatorGlobals>().doorOpen = true;
                playerFSM.SendEvent("DoorOpen");
                playerFSM.SendEvent("Floor0");
            }
        }
    }

}
