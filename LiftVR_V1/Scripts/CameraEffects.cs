using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{

    public ElevatorMovement movementScript;

    public float maxMovementAdjustment;

    public int timeBetweenShifts;

    private float adjustment = 0f;

    private int timer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var liftSpeed = movementScript.liftSpeedCurrent;
        if(liftSpeed != 0 && timer % timeBetweenShifts == 0)
        {
            if(adjustment > 0)
            {
                adjustment = -Mathf.Abs(maxMovementAdjustment * (liftSpeed / movementScript.liftSpeedMax));
            }
            else
            {
                adjustment = Mathf.Abs(maxMovementAdjustment * (liftSpeed / movementScript.liftSpeedMax));
            }
            
            //Up or down effect on the camera based on the elevator speed
            transform.position = new Vector3(transform.position.x, adjustment, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        timer++;
    }
}