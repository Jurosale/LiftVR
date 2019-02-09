using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronAudio : MonoBehaviour {

    public AudioSource patronMouth;
    public string patronName;
    public string dayName;
    public string currentAudio;

    private GameObject elevatorManager;
    private float patronVolume;

	// Use this for initialization
	void Start () {

        elevatorManager = GameObject.FindGameObjectWithTag("HotelManager");

        if (patronMouth == null) {
            Debug.LogError(gameObject.name + " does not have a mouth assigned");
        }
        else {
            patronVolume = patronMouth.volume;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(elevatorManager.GetComponent<FloorManager>().doorOpen == false && GetComponent<PatronMovement>().state == "leaving") {
            patronMouth.volume = patronVolume - 0.6f;
        }
	}

    public void playDialogue(string dialogue) {
        var path = "Dialogue/" + patronName + "/" + dayName + "/" + dialogue;
        currentAudio = dialogue;
        patronMouth.clip = Resources.Load(path) as AudioClip;
        patronMouth.Play();
    }
}
