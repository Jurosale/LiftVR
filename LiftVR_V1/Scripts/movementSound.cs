using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementSound : MonoBehaviour {

    public float timeToEngine = 5.0f;
    private bool revvingUp = false;
    private bool elevatorStopped = true;
    private float timer = 0.0f;

    public float liftSpeed = 0f;
    private float liftSpeedMax;

    public string elevatorStartSound;
    public string elevatorEngineSound;
    public string elevatorStopSound;

    [Range(0f, 1f)]
    public float noPatronVolume;

    [Range(0f, 1f)]
    public float patronVolume;

    private GameObject activeMoveSound;

    void Start(){
        liftSpeedMax = GetComponent<ElevatorMovement>().liftSpeedMax;
    }

    // Update is called once per frame
    void Update () {

        if (liftSpeed != 0) {
            //If elevator has just started moving
            if (elevatorStopped) {
                elevatorStartSound.PlaySound();
                revvingUp = true;
                elevatorStopped = false;
            }
        }
        else if(liftSpeed == 0 && elevatorStopped == false) {
            Debug.Log("Wow");
            //Play the stopping sound
            elevatorStopSound.PlaySound();

            //Stop playing the elevator engine sound
            activeMoveSound.GetComponent<SoundGroup>().pingSound();
            elevatorStopped = true;
        }

		if(revvingUp == true) {
            if(timer < timeToEngine) {
                timer += 0.1f;
            }
            else {
                elevatorEngineSound.PlaySound();
                activeMoveSound = GameObject.Find("_SFX_ElevatorMovementSound");
                revvingUp = false;
            }
        }
	}
}
