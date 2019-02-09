using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    public bool doorOpen;
    public int floorPos = -1;

    public GameObject[] floors;                             //[] is the gameObject floor holder
    private Vector3[] floorStarts;

    private int activeFloorIndex;

    [SerializeField]
    private GameObject elevatorWaypoint;
    [SerializeField]
    private GameObject dateWaypoint;

    private GameObject elevatorManager;

    void Start() {
        elevatorManager = GameObject.FindGameObjectWithTag("ElevatorManager");

        floorStarts = new Vector3[floors.Length];
        for(var i = 0; i < floors.Length; i++) {
            Debug.Log(floors.Length);
            floorStarts[i] = floors[i].transform.position;
        }
    }

    //Called from elevator movement when new floor is reached
    public void loadNewFloor(int targetFloor)
    {
        if (floors[activeFloorIndex] != floors[targetFloor])
        {
            //Turn off the previously active floor
            floors[activeFloorIndex].SetActive(false);

            //Bring the previous floor to its original starting position
            Debug.Log(floorStarts);
            floors[activeFloorIndex].transform.position = floorStarts[activeFloorIndex];

            //Turn on the next floor
            floors[targetFloor].SetActive(true);
            floorPos = targetFloor;
        }

        activeFloorIndex = targetFloor;
    }

    public GameObject fetchElevatorWaypoint()
    {
        return elevatorWaypoint;
    }

    public GameObject fetchDateWaypoint() {
        return dateWaypoint;
    }

    public GameObject fetchFloorWaypoint(int floorNumber)
    {
        return floors[floorNumber].transform.Find("Waypoint").gameObject;
    }

    public GameObject fetchFloorWaypoint2(int floorNumber, int waypointIter)
    {
        if (floors[floorNumber].transform.Find("Waypoint" + waypointIter.ToString()) != null) {
            return floors[floorNumber].transform.Find("Waypoint" + waypointIter.ToString()).gameObject;
        }
        else {
            return null;
        }
    }

    void Update()
    {
        //Floors moving while elevator movement
        var curSpeed = elevatorManager.GetComponent<ElevatorMovement>().liftSpeedCurrent;
        if (curSpeed != 0) {
            floors[activeFloorIndex].transform.position = new Vector3(floors[activeFloorIndex].transform.position.x, floors[activeFloorIndex].transform.position.y + -(2 * curSpeed), floors[activeFloorIndex].transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("menu");
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            SceneManager.LoadScene("main");
        }
    }
}
