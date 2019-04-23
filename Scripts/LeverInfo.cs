using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LeverInfo : MonoBehaviour {

    [Tooltip("The Degrees of Each Floor for the Lever")]
    [Header("Floors")]
    
       public float[] floors;
    /// -122
    /// -111
    /// -100
    /// -89
    /// -78
    /// -67
    /// -57
    FloorManager floormanager;
    ElevatorMovement em;

    public bool setToFloor = false; // when stopped on a floor and lever disabled
    private int currentFloor = -1; //Used to prevent loading a floor more than once
    private int lastLoadedFloor = -2; //Used to prevent loading a floor more than once (must be different than currentFloor)

    public state.floor chosenOne;

    [SerializeField]
    float leverRotation;

    float max;
    float min;

    public GameObject dial;

    VRTK_InteractableObject leverVRTK;
    private float positionToRotation;
    private bool reset = false;
    public float resetSpeed = 0.1f;

    float section; // = max+min/floors)/3
    float floorNum = 6;

    int targetFloor;
    float targetAngle;

    HingeJoint hj;
    JointSpring js;
   
    bool wasGrabbedLastFrame = false;
    bool spring = false;

    // Use this for initialization
    void Start() {
        floormanager = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<FloorManager>();
        em = GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<ElevatorMovement>();

        hj = GetComponent<HingeJoint>();
        js = GetComponent<HingeJoint>().spring;
        max = hj.limits.max;
        min = hj.limits.min;
       // leverRotation = GetComponent<HingeJoint>().angle;

        // starting lever rotation
        leverRotation = floors[(int)em.GetComponent<ElevatorMovement>().floorPos];
        
        leverVRTK = GetComponent<VRTK_InteractableObject>();

        section = (Mathf.Abs(max - min) / floorNum);
        setFloors();
    }

    // Update is called once per frame
    void Update() {
        if (section <= 0) section = (Mathf.Abs(max - min) / floorNum);

        if (!reset && leverRotation != floors[(int)em.GetComponent<ElevatorMovement>().floorPos]) {
            // this means we have to reset the fckin lever to correct
            // meant on editor start mode
               
                leverRotation = floors[(int)em.GetComponent<ElevatorMovement>().floorPos];
            reset = true;     
        } else {
            leverRotation = GetComponent<HingeJoint>().angle;
        }

        if (leverVRTK.IsGrabbed()) {
            wasGrabbedLastFrame = true;
           // if (reset) {
           //     reset = false;
          //  }
            setToFloor = false;
        } else {
          //  reset = true;
        }

        //start 57 to 122
        //L = ~70
        //


        //Check if rotation is on a floor
        if (wasGrabbedLastFrame && !leverVRTK.IsGrabbed() && !setToFloor) { //This is an infinite loop
            float targetAngle = -9999f;

            // go through array of floors and see which one is the closest
            //But not just closest, find the highest value that is less than the lever

            for (int i = 0; i < floors.Length; i++) {
                //Make sure this value is lower than current lever
                if (floors[i] < (leverRotation + 0.2f)) {
                    //If it is, check if it is lower than the current highest one that is lower than the lever
                    if (floors[i] > targetAngle) {
                      
                        targetAngle = floors[i];
                      //  print("found a target: setting value: " + targetAngle);
                        targetFloor = i; //may not need this
                    }
                }
            }

            //Dont need to load this floor
            if (currentFloor == targetFloor) {
                print("CURRENT FLOOR IS ALREADY TARGET FLOOR, RETURN");
                spring = false;
                return;
            }

            //We've identified a new target floor
            //Lerp to the new rotation
            float a = floors[targetFloor] - section;
            float b = floors[targetFloor] + section;
             print("lever rotation " + leverRotation + " FLOORS[TARGETFLOOR] " + floors[targetFloor] + " target floor + section " + b);
            // if rotation in between floors still
            // set target angle to target floor
            // this ideally would mean that the floor is setting
            if (((leverRotation + 0.2f) >= targetAngle) && (leverRotation < (targetAngle + section))) {
                currentFloor = targetFloor;
                em.moveTowardsFloor((state.floor)currentFloor);
                spring = true;

                setToFloor = true;
            } else {

            }
        }


        if (!leverVRTK.IsGrabbed())
        {
            wasGrabbedLastFrame = false;
            
        } 
         
        if(spring && wasGrabbedLastFrame && !leverVRTK.IsGrabbed()) {
            hj.useSpring = true;
            springLevertoGoal(targetAngle);
        } else {
            
            hj.useSpring = false;

        }

    }

    void springLevertoGoal(float angle) {
        js.targetPosition = angle;
        hj.spring = js;

        if (leverRotation == angle) {
            spring = false;
            print("reached target angle");
        }
        spawnFloor();
    }

    public void spawnFloor() {
      //  floormanager.loadNewFloor((state.floor)targetFloor, false);
        setToFloor = false;
    }

    public void activefloor() {
        for (int i = 0; i< floormanager.floors.Length; i++)
        {
            
        }
    }

    public void setFloors() {
        for (int i = 0; i < floors.Length; i++) {
            float angle = min + (section*i);
            floors[i] = angle;
            max = floors[floors.Length-1];
            //hj.limits.max = max;
        }
    }

}
