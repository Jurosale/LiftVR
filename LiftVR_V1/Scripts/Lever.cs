using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

/*
 * This script is attached to the Manager
 * It keeps track of the value of the lever.
 * 
 * >.5 means that it is going up 
 * =.5 means that it is in the middle
 * <.5 means that it is going down
 */ 

public class Lever : MonoBehaviour {

	public GameObject lever;
	public GameObject handle;

	bool raising;

	[SerializeField]
	SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device device { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}

	public float currValue;

	// Use this for initialization
	void Start () {
		if (!lever) lever = GameObject.FindGameObjectWithTag ("lever");
		if (!handle) handle = GameObject.FindGameObjectWithTag ("handle");
	}
	
	// Update is called once per frame
	void Update () {
		if (!lever) lever = GameObject.FindGameObjectWithTag ("lever");
		if (!handle) handle = GameObject.FindGameObjectWithTag ("handle");

//		currValue = handle.GetComponent<NVRLever> ().CurrentValue;

		if (currValue < .4f) {
			// value is going down
			Debug.Log("Going down! " + currValue);
		} else if (currValue > .6f) {
			// value is going up
			Debug.Log("Going up! " + currValue);
		} else {
			Debug.Log("Stopped! " + currValue);
		}

		//if (device.GetPress (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && collided) {
			//foreach (GameObject hold in holds) {
		//		if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x) {
		//			Debug.Log ("hell yeah get ready to lift");
		//			raising = true;

					// Move the door according to the current y position of the controller
					//door.transform.position = new Vector3 (initX, (door.transform.position.y - hold.transform.position.y)+trackedObj.transform.position.y, initZ);


			//	}
		//	}
		//}
	}
}

