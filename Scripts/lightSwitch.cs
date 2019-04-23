using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class lightSwitch : MonoBehaviour {

    public bool OnInMorning = true;
    public bool OnInDay = false;
    public bool OnInEvening = true;
    public bool OnAtNight = false;
	
	// Update is called once per frame
	void Update () {
        var currentTime = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<timeOfDay>().fetchTime();
        GetComponent<Light>().enabled = false;

        if (currentTime == state.dayCycle.Morning && OnInMorning)
        {
            GetComponent<Light>().enabled = true;
        }
        else if (currentTime == state.dayCycle.Midday && OnInDay)
        {
            GetComponent<Light>().enabled = true;
        }
        else if (currentTime == state.dayCycle.Evening && OnInEvening)
        {
            GetComponent<Light>().enabled = true;
        }
        else if (currentTime == state.dayCycle.Night && OnAtNight)
        {
            GetComponent<Light>().enabled = true;
        }
    }
}
