using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grabbing : MonoBehaviour {

    private Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    //For Permanant Haptic Guidance
    private SteamVR_ControllerManager CameraHead;

    private GameObject lever;

    private GameObject objDoor;

    private GameObject menu;

    private float leverTimer;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        lever = GameObject.FindGameObjectWithTag("lever");
        objDoor = GameObject.FindGameObjectWithTag("door");
        menu = GameObject.FindGameObjectWithTag("Menu");

        leverTimer = 3.0f;

        CameraHead = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>();
        Debug.Log(gameObject.tag + controller.index);
        if(gameObject.tag == "leftControl") {
            CameraHead.leftControlIndex = (int)controller.index;
        }
        else {
            CameraHead.rightControlIndex = (int)controller.index;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (CameraHead == null)
			CameraHead = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>();
		if (trackedObj == null)
			trackedObj = GetComponent<SteamVR_TrackedObject>();
		if (lever == null)
			lever = GameObject.FindGameObjectWithTag("lever");
		if (objDoor == null)
			objDoor = GameObject.FindGameObjectWithTag("door");
		if (menu == null) 
			menu = GameObject.FindGameObjectWithTag("Menu");
		
        //checks if door is open
        doorInteraction door = objDoor.GetComponent<doorInteraction>();
        if (door.open == true) { leverTimer = 0f; }
        else { leverTimer -= Time.deltaTime; }
        if (controller == null) {
            return;
        }

        if (controller.GetPressDown(triggerButton))
        {
            if (SceneManager.GetActiveScene().name == "main") {
                lever.GetComponent<LeverRange>().attemptGrab(this.gameObject, door.open, leverTimer);
                objDoor.GetComponent<doorInteraction>().attemptGrab(this.gameObject);
            }

            if (SceneManager.GetActiveScene().name == "menu") {
                menu.GetComponent<menuButtonController>().attemptGrab();
            }
        }
        else if (controller.GetPressUp(triggerButton))
        {
            if (SceneManager.GetActiveScene().name == "main") {
                lever.GetComponent<LeverRange>().attemptRelease();
                objDoor.GetComponent<doorInteraction>().attemptRelease();
            }
        }  
    }
}
