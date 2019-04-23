using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class GazeBehaviour : MonoBehaviour {

    public GameObject cameraEye;
    public DeviceInfo deviceL;
    public DeviceInfo deviceR;

    float lookedAt = 3f;
    bool gazeTrigger = false;

    float counter = 0f;
    float dist = 50f;
    int gazeEvent = 1;

    int patronLayer = 1 << 8;

    // Contains a HMD tracked object that we can use to find the user's gaze
    Transform hmdTrackedObject = null;
    Transform fingerPoint = null;


    GameObject playerObject;
    PlayMakerFSM playerFSM;

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("PlayerManager");
        playerFSM = playerObject.GetComponent<PlayMakerFSM>();
    }

    private void Update() {


        //if (camera.name == "Camera (eye)") {
            // attached to head so do head things
        if (deviceL.trigger) {
            HandPoint(deviceL.gameObject);

        }
        if (deviceR.trigger) {
            HandPoint(deviceR.gameObject);
        }

    }
    public void HandPoint(GameObject obj)
    {
        print("sickening");
        RaycastHit objHit;
        Vector3 fwd = obj.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(obj.transform.position, fwd, Color.green);
        counter = 0;
        if (Physics.Raycast(obj.transform.position, fwd, out objHit, Mathf.Infinity, patronLayer))
        {
            if (counter < lookedAt)
            {
                print("increased");
                counter++;
            }
            else
            {
                // GAZE
                print("you are gazing! ");
                gazeTrigger = true;
                playerFSM.SendEvent("False");
                return;
            }
        }
        //event
        playerFSM.SendEvent("False");
        gazeTrigger = false;
        print("boooooooooooooooooooooooooooooooy");
        return;
    }
    
    // hey did you look at them long enough

}
