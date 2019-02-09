using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class handGestures : MonoBehaviour {


	/*
	 * Deals with changing hand model to pointing (trigger)
	 * 
	 */ 

	[SerializeField]
	SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device device { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}

	public Material testred;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
			Debug.Log ("trigger down");
			trackedObj.GetComponentInChildren<Renderer>().material = testred;
		}
	}
}
