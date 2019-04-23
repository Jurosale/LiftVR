using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

public class MockPatronAI : MonoBehaviour {


    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown("1")) {
            Debug.Log("DoorOpen");
            //playerFSM.SendEvent("DoorOpen");

            PlayMakerFSM.BroadcastEvent("DoorOpen");
            PlayMakerFSM.BroadcastEvent("Floor" + (int)ElevatorGlobals.currentFloor);
            print("Floor" + (int)ElevatorGlobals.currentFloor);
        }
        if (Input.GetKeyDown("/"))
        {
            Debug.Log("DoorClose");
            PlayMakerFSM.BroadcastEvent("DoorClose");
        }
        if (Input.GetKeyDown("2"))
        {
            Debug.Log("Interaction(nod)");
            PlayMakerFSM.BroadcastEvent("Interaction(nod)");

        }
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("Interaction(shakeHead)");
            PlayMakerFSM.BroadcastEvent("Interaction(shakeHead)");

        }
        if (Input.GetKeyDown("4"))
        {
            Debug.Log("Interaction(eyeContact)");
            PlayMakerFSM.BroadcastEvent("Interaction(eyeContact)");

        }
        if (Input.GetKeyDown("5"))
        {
            Debug.Log("Interaction(lookAtPoster1)");
            PlayMakerFSM.BroadcastEvent("Interaction(lookAtPoster1)");

        }
        if (Input.GetKeyDown("6"))
        {
            Debug.Log("Interaction(lookAtPoster2)");
            PlayMakerFSM.BroadcastEvent("Interaction(lookAtPoster2)");

        }
        if (Input.GetKeyDown("7"))
        {
            Debug.Log("Interaction(lookAtPoster3)");
            PlayMakerFSM.BroadcastEvent("Interaction(lookAtPoster3)");

        }
        if (Input.GetKeyDown("8"))
        {
            Debug.Log("CorrectFloor)");
            PlayMakerFSM.BroadcastEvent("CorrectFloor");

        }
        if (Input.GetKeyDown("9"))
        {
            Debug.Log("IncorrectFloor");
            PlayMakerFSM.BroadcastEvent("IncorrectFloor");

        }
        if (Input.GetKeyDown("0"))
        {
            Debug.Log("NoResponse");
            PlayMakerFSM.BroadcastEvent("NoResponse");

        }
        if (Input.GetKeyDown("."))
        {
            Debug.Log("Skip");
            PlayMakerFSM.BroadcastEvent("Skip");

        }
    }
}
