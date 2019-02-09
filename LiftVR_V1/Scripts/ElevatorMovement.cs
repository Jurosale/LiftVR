using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    [Header("Global Variables")]
    public float floorPos;                              //The number floor the elevator is on. 
    public float liftSpeedCurrent;                      //The current speed of the elevator
    public bool doorOpen;                               //If the elevator door is open, TRUE is open FALSE is closed

    [Header("Movement Variables")]
    
    public float liftSpeedMax;                          //The maximum speed to elevator can reach
    private bool initialArrival;

    [Header("Magnet Variables")]
    public float floorRounding;                         //How close to the floor the elevator must be for it to round
    private float previousFloorPos;
    private float lastPassedFloor = -1;
    private bool magnet;
    public float magnetForce;

    [Header("Effects Variables")]
    public int maxVibration;

    [Header("Sounds")]
    public string floorPassingSound;

    [Header("New Lever")]
    private int targetFloor;

    private GameObject lever;
    private GameObject hotelManager;

    //Used for hitting the max or min of the elevator bounds
    private bool firstHit = false;

    // Use this for initialization
    void Start () {
        floorPos = 0f;
        liftSpeedCurrent = 0f;
        initialArrival = false;

        magnet = false;

        lever = GameObject.FindGameObjectWithTag("lever");
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (lever == null)
			lever = GameObject.FindGameObjectWithTag("lever");
		if (hotelManager == null)
			hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
		
        //Add current speed
        floorPos += liftSpeedCurrent;

        //If not a target destination
        if(floorPos != targetFloor) {
            if (Mathf.Abs(floorPos - targetFloor) > liftSpeedMax * 1.5) {
                if (floorPos < targetFloor) {
                    liftSpeedCurrent = liftSpeedMax;
                }
                else {
                    liftSpeedCurrent = -liftSpeedMax;
                }
            }
            else {
                floorPos = targetFloor;
                liftSpeedCurrent = 0;
            }
            initialArrival = true;
        }
        else if(initialArrival){
            //We're at the target floor
            arriveAtFloor(targetFloor);
        }


        //If elevator is moving
        if (liftSpeedCurrent != 0) {
            //Haptic Feedback
            /*
            var deviceIndex1 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            SteamVR_Controller.Input(deviceIndex1).TriggerHapticPulse((ushort)Mathf.FloorToInt(Mathf.Abs(liftSpeedCurrent) / liftSpeedMax * maxVibration));
            var deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            SteamVR_Controller.Input(deviceIndex2).TriggerHapticPulse((ushort)Mathf.FloorToInt(Mathf.Abs(liftSpeedCurrent) / liftSpeedMax * maxVibration));
            */

            //Check for passing floors
            if ((floorPos == Mathf.Round(floorPos) || ((floorPos - Mathf.Round(floorPos)) * (previousFloorPos - Mathf.Round(floorPos)) < 0)) && Mathf.Round(floorPos) != lastPassedFloor) {
                //We're passing a floor
                Debug.Log("passing floor");
                floorPassingSound.PlaySound(transform.position);
                lastPassedFloor = Mathf.Round(floorPos);
            }

            //Change this SSSSSSSSSSSSSSSSSSSSSSHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIITTTTTTTTTTTTTTTTTTTTTTTTTTTTTT <-
            hotelManager.GetComponent<FloorManager>().floorPos = -1;
        }

        //Send the elevator speed to the movementSound Script
        GetComponent<movementSound>().liftSpeed = liftSpeedCurrent;
    }
    public void newDoorTarget(int target) {
        Debug.Log("New target floor: " + target);
        //Called from lever rotation when set to a different floor
        if (target != targetFloor) {
            targetFloor = target;
        }
    }

    public void doorOpened()
    {
        doorOpen = true;
        hotelManager.GetComponent<FloorManager>().doorOpen = true;
    }

    public void doorClosed()
    {
        doorOpen = false;
        hotelManager.GetComponent<FloorManager>().doorOpen = false;
    }

    public void arriveAtFloor(int floor) {
        floorPassingSound.PlaySound(transform.position);
        floorPos = floor;

        //Load in the floor stopped at
        hotelManager.GetComponent<FloorManager>().loadNewFloor(floor);

        initialArrival = false;

        if(targetFloor != floor) {
            targetFloor = floor;
        }
    }
}
