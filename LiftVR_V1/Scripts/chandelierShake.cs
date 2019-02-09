using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chandelierShake : MonoBehaviour {

    private GameObject hotelManager;
    private Animator anim;
    private AudioSource sound;

    private Vector3 startPos;

    private bool reset;

    [Range(0f, 1f)]
    public float noPatronVolume;

    [Range(0f, 1f)]
    public float patronVolume;

    // Use this for initialization
    void Start () {
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        reset = false;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(hotelManager.GetComponent<DayManager>().patronPresent == true) {
            sound.volume = patronVolume;
        }
        else {
            sound.volume = noPatronVolume;
        }

		//if Elevator is moving, then chandelier should be moving
        if(hotelManager.GetComponent<FloorManager>().floorPos == -1)
        {
            anim.enabled = true;
            sound.enabled = true;

            //while moving, play sound occasionally
        }
        //if Elevator has just stopped moving, then we need to lerp back to starting point
        else if (reset == false)
        {
            anim.enabled = false;
            sound.enabled = false;
            transform.position = Vector3.Lerp(transform.position, startPos, 0.01f);
        }
	}
}
