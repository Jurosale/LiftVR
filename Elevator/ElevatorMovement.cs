using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    //The floor position of the elevator with 0 being basement and 6 being restaurant
    //Used for movement
    
    //The numerical value of the elevator's location
    public float floorPos = 0f;

    [Header("Movement Variables")]
    [Range(0.01f, 0.05f)]
    public float liftSpeedMax;                  //The Max speed the elevator can move
    [Range(1.0001f, 1.001f)]
    public float accelerationIncrement;         //The amount at which elevator speed increases until it reaches max speed
    [Range(0.1f, 1f)]
    public float decelerateRange = 0.5f;

    

    private float liftSpeedCurrent = 0f;
    private int increment = 0;
    
    private bool accelerating = false;
    private int direction = 1;
    private state.floor targetFloor;

    [Header("Sounds")]
    public AudioClip floorPassingSound;
    public AudioClip floorArriveSound;

    //References
    private ElevatorGlobals globals;

    FloorManager fm;

	// Use this for initialization
	void Start () {
        //Fetch References
        globals = this.GetComponent<ElevatorGlobals>();
        fm = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<FloorManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            moveTowardsFloor(state.floor.Restaurant);

        if (globals.moving)
        {
            if((floorPos < (int)targetFloor && direction == 1) || (floorPos > (int)targetFloor && direction == -1))
            {
                //Apply Movement
                floorPos += direction * liftSpeedCurrent;

                //Check if we should stop accelerating
                if(accelerating && (floorPos > (int)targetFloor - decelerateRange && floorPos < (int)targetFloor + decelerateRange))
                {
                    accelerating = false;
                }

                if (accelerating)
                {
                    //If we are below max speed and still accelerating
                    if(liftSpeedCurrent < liftSpeedMax)
                    {
                        //Speed is Exponential
                        liftSpeedCurrent = Mathf.Pow(accelerationIncrement, increment) - 1;
                        increment++;
                        //Debug.Log("Accelerating to Max");
                    }
                    else
                    {
                        //Debug.Log("At Max Speed");
                    }
                }
                //If we are decelerating
                else
                {
                    if(liftSpeedCurrent > 0)
                    {
                        increment--;
                        liftSpeedCurrent = Mathf.Pow(accelerationIncrement, increment) - 1;
                        //Debug.Log("Decelerating to Min");
                    }
                }
            }
            //If we have arrived at the target floor
            else
            {
                if(liftSpeedCurrent > 0)
                {
                    increment--;
                    liftSpeedCurrent = Mathf.Pow(accelerationIncrement, increment) - 1;
                 //   Debug.Log("Coming to Stop");
                }
                else
                {
                    globals.moving = false;
                    floorPos = (int)targetFloor;
                    //fm.loadNewFloor(targetFloor, false);
                    ElevatorGlobals.currentFloor = targetFloor;
                    //   Debug.LogWarning("Arrived at Floor");
                }
            }
        }
	}

    //Called from the lever to have the elevator move towards the targeted floor
    public void moveTowardsFloor(state.floor target)
    {
        floorPos = (int)ElevatorGlobals.currentFloor;


        if(floorPos < (int)target)
        {
            direction = 1;
        }
        else if(floorPos > (int)target)
        {
            direction = -1;
        }
        else
        {
            return;
        }
        globals.moving = true;
        accelerating = true;
        targetFloor = target;
    }
}
