using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronVolume : MonoBehaviour {

    private GameObject hotelManager;
    private AudioSource sound;

    [Range(0f, 1f)]
    public float noPatronVolume;

    [Range(0f, 1f)]
    public float patronVolume;

    // Use this for initialization
    void Start () {
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if(hotelManager.GetComponent<DayManager>().patronPresent == false) {
            sound.volume = noPatronVolume;
        }
        else {
            sound.volume = patronVolume;
        }
	}
}
