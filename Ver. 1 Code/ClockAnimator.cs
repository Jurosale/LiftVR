using UnityEngine;
using System.Collections;

public class ClockAnimator : MonoBehaviour {
    //This is here as a simple way to hide the clock if we dont want it at the moment. set in 
    //Manager/Clcok animator script/"Show Clock" field and toggle true/false
    public bool showClock;
    private const float
        hoursToDegrees = 360f / 12f, //makes the hour arm move at a rate of 1 hour in 1 minute
        minutesToDegrees = 360f / 60f;  //makes the minute arm move at a rate of 1 minute per second 

    public Transform hours, minutes;

    void Update() {
        //if we don't want clock, hide it
        if (showClock == false) {
            hours.localPosition = new Vector3(100,100,100);
            minutes.localPosition = new Vector3(100, 100, 100);
        }
        //otherwise simulates the movement of a real face clock at a rate of 1 minute in game = 1 second in realtime
        else {
            float time = Time.timeSinceLevelLoad;
            float minute = time % 60f;
            float hour = time / 60f;

            hours.localRotation =
                Quaternion.Euler(0f, 0f, hour * +hoursToDegrees - 6 * hoursToDegrees);
            minutes.localRotation =
                Quaternion.Euler(0f, 0f, minute * +minutesToDegrees);
        }
    }
}
