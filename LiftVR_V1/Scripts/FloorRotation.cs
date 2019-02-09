using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRotation : MonoBehaviour {

    GameObject Manager;
    private float dialFloorPos;

	// Use this for initialization
	void Start () {
		Manager = GameObject.FindGameObjectWithTag("ElevatorManager");
    }

    // Update is called once per frame
    void Update () {
        //If Floor has been changed and dial not updated
        if(dialFloorPos != Manager.GetComponent<ElevatorMovement>().floorPos)
        {
            //90 is floor 0 and -90 is max floor (5)
            transform.rotation = Quaternion.Euler(-Manager.GetComponent<ElevatorMovement>().floorPos * 30, 180, 0);
            dialFloorPos = Manager.GetComponent<ElevatorMovement>().floorPos;
        }

	}
}
