using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handpointRaycast : MonoBehaviour {

    float lookedAt = 3f;
    bool gazeTrigger = false;

    float counter = 0f;
    float dist = 50f;
    int gazeEvent = 1;

    int patronLayer = 1 << 8;


    GameObject playerObject;
    PlayMakerFSM playerFSM;

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("PlayerManager");
        playerFSM = playerObject.GetComponent<PlayMakerFSM>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
