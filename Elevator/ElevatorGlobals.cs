using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ElevatorGlobals : MonoBehaviour {

    [Header("Read Only Variables")]
    [ReadOnly] public bool moving = false;
    [ReadOnly] public bool doorOpen = false;
    [ReadOnly] public static state.floor currentFloor;

    private state.floor previousFloor;

    private FloorManager fM;

    GameObject playerObject;
    PlayMakerFSM playerFSM;

    bool sent = false;

	// Use this for initialization
	void Start () {
        playerObject = GameObject.FindGameObjectWithTag("PlayerManager");
        playerFSM = playerObject.GetComponent<PlayMakerFSM>();
        previousFloor = state.floor.Empty;
    }
	
	// Update is called once per frame
	void Update () {
        fM = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<FloorManager>();

        if (currentFloor != previousFloor)
        {
            if (Application.isEditor)
            {
                fM.loadNewFloor(currentFloor, false);
            }
            else
            {
                fM.loadNewFloor(currentFloor, true);
            }
            previousFloor = currentFloor;
        }

    }
}
