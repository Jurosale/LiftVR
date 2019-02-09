using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorAmbientSound : MonoBehaviour {

    private bool soundPlaying;
    private ElevatorMovement elevatorManager;

    [Range(0f, 1f)]
    public float doorOpenVolume;
    [Range(0f, 1f)]
    public float doorClosedVolume;

    // Use this for initialization
    void Start () {
        elevatorManager = GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<ElevatorMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (elevatorManager.doorOpen) {
            GetComponent<AudioSource>().volume = doorOpenVolume;
        }
        else {
            GetComponent<AudioSource>().volume = doorClosedVolume;
        }
	}
}
