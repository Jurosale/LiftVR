using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author Daniel Chamberlin 1/12/2019
//New Movement System designed to work with Playmaker and easy to set-up view in Editor

public class PatronMove : MonoBehaviour {

    //Reference to the location of the next waypoint
    private GameObject targetWaypoint;

    [Range(0.5f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
    public float rotationSpeed = 5f;

    public enum currentAction { idle, walking, talking };

    public currentAction state = currentAction.idle;

    [Header("Footsteps")]
    public string footstepSound;
    public float footstepGap;
    private float footstepTimer;

    // Use this for initialization
    void Start () {
		moveOnWaypoint("ExitElevator");
	}
	
	// Update is called once per frame
	void Update () {
        if (targetWaypoint != null)
        {
            float step = walkSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, step);
            if (transform.position == targetWaypoint.transform.position)
            {
                if (targetWaypoint.GetComponent<patronWaypoint>().endNode)
                {
                    //despawnPatron();
                    targetWaypoint = null;
                }
                if (targetWaypoint.GetComponent<patronWaypoint>().nextNode != null)
                {
                    targetWaypoint = targetWaypoint.GetComponent<patronWaypoint>().nextNode;
                }
                else
                {
                    targetWaypoint = null;
                }
            }
        }
    }

    //@Derrek, move this to Patron Functions, takes a pathName as a string for now. May want to change this
    public void moveOnWaypoint(string pathName)
    {
        var pathOptions = getFloorWaypoints();
        foreach (GameObject option in pathOptions)
        {
            //The path which has been requested
            if(option.name == pathName)
            {
                targetWaypoint = option;
            }
        }
    }

    //Check the Hotel Floor to see which Waypoint Paths are Available
    //Defined in the Object of each floor in the hotel
    //Script FloorWarpoints
    //Array floorPaths
    private GameObject[] getFloorWaypoints()
    {
        var hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        GameObject floorObject = null;

        foreach (Transform child in hotelManager.transform)
        {
            if (child.gameObject.activeSelf)
            {
                floorObject = child.gameObject;
            }
        }

        if(floorObject != null)
        {
            return floorObject.GetComponent<FloorWaypoints>().floorPaths;
        }
        else
        {
            return null;
        }
    }
}
