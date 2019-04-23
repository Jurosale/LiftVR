using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DevelopmentTools : MonoBehaviour {

    [Header("Elevator")]
    public state.floor TeleportToFloor;
    public bool OpenDoor;

    [Header("Time Of Day")]
    public state.dayCycle ChangeTime;

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        //Floor
        if (TeleportToFloor == state.floor.Empty) { TeleportToFloor = state.floor.Restaurant; }
        var eleManager = GameObject.FindGameObjectWithTag("ElevatorManager");
        var eleGlobals = eleManager.GetComponent<ElevatorGlobals>();
        ElevatorGlobals.currentFloor = TeleportToFloor;
        eleGlobals.doorOpen = true;
        OpenDoor = false;
        //Door Open
        if (OpenDoor == false && eleGlobals.doorOpen == true)
        {
            //We want to Close the Door
          //  eleManager.GetComponent<ElevatorDoor>().OpenDoor();
        }
        else if(OpenDoor == true && eleGlobals.doorOpen == false)
        {
            //We want to Open the Door
         //   eleManager.GetComponent<ElevatorDoor>().CloseDoor();
        }

        //Time of Day
        GameObject.FindGameObjectWithTag("HotelManager").GetComponent<timeOfDay>().setTime(ChangeTime);
    }
}
