using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the door
 * 
 * When the players controller comes in contact with the holds,
 * 	If the door is closed
 * 		the player raises it when it has trigger pressed and raises arm
 * 	If the door is open
 * 		the player closes door when it has trigger pressed and lowers arm
 * 
 */

/* This script was based off of the existing door interaction script, but with additional support to include a seperate pulley object. In its current 
* state, a door and pulley object should be added in editor, with the script attached to the pulley objetct (not necessary, but helpful). The starting 
* positions and scales of the pulley and door should be arbitrary (it should still work regardless of starting positions/ sizes)This version still 
* requires VR controller support, and currently functions by using the editor to update the current Y position of the pulley object. A temp bool variable 
* called "holding" determines whether or not the player is currently holding the pulley. if this is the case, then the Y pos of the door are mirrored by 
* the pulley. while the player is not holding the pulley (!holding) the door and pulley will automatically transfer to a standing position of either open 
* or closed depending on it's current state. Lowering or raising the door/ pulley to an invalid Y position will cause both objects to return to a valid 
* position. In order to complete this script, it just needs to determine when the player is holding the pulley, and to update the Y pos of the pulley in regard to 
* the location of the player's controller. Some additional functionality may also be added (SFX, animations, etc.)
*/

public class doorInteraction : MonoBehaviour {

    private GameObject door, pulley;
    private GameObject hold;
    private GameObject doorOpen;
    private GameObject rope;
	public static bool nearDoor;

    bool holding; // temp var

    bool lifting;
    bool closing;
    public bool open = false;

    private bool grabbingUp = false;
    private bool grabbingDown = false;

    //Resting Positions
    float initDoorX, initDoorY, initDoorZ;
    float doorOpenY = 3f;
    float doorBeginLerpUpY, doorBeginLerpDownY;

    float initPulleyX, initPulleyY, initPulleyZ;
    float pulleyOpenY;

    //current positions
    float doorX, doorY, doorZ;
    float pulleyX, pulleyY, pulleyZ;

    float mirror, scale, autoSpeed;

    float currY;
    float initX;
    float initY;
    float initZ;

    public float midPoint = 2.4f;
    public float highPoint = 3.2f;

    Vector3 frame1;
    Vector3 frame2;
    Vector3 frame3;

    public slidingDoor2 slidingDoor2;
	//public DisableGesture disableGesture;

    public string doorLoopSFX;
    public string startSoundSFX;
    public string stopSoundSFX;
    bool playing = false;

    [HideInInspector]
    public static bool handInRange = false;
    private bool grabbed = false;
    private GameObject grabbingHand;

    private GameObject manager;

    [Header("Holding Vibration")]
    public int holdingVibrationMax;
    private float previousHeight;

    [Header("Jiggle Effect")]
    public float jiggleStrength = 10;
    private bool jiggling = false;
    private int jiggleTimer = 0;
    public int jiggleLength = 50;
    [Range(1, 10)]
    public int jiggleSpeed = 6;
    private int jiggleDivisor;

	public GameObject camRig;
    public string jiggleSound;

    // Use this for initialization
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("door");
        hold = GameObject.FindGameObjectWithTag("doorHold");
       // doorOpen = GameObject.FindGameObjectWithTag("openDoor");
        rope = GameObject.FindGameObjectWithTag("rope");
		camRig = GameObject.FindGameObjectWithTag("Player");

        initDoorX = door.transform.localPosition.x;
        initDoorY = door.transform.localPosition.y;
        initDoorZ = door.transform.localPosition.z;

        doorOpenY = (initDoorY + door.transform.localScale.y);
        doorBeginLerpDownY = (initDoorY + door.transform.localScale.y - .5f);
        doorBeginLerpUpY = (initDoorY + .5f);

        scale = 4f; // lower movement required for pulley
        initPulleyX = pulley.transform.position.x;
        initPulleyY = pulley.transform.position.y;
        initPulleyZ = pulley.transform.position.z;
        pulleyOpenY = initPulleyY - (door.transform.localScale.y/scale);

        manager = GameObject.FindGameObjectWithTag("ElevatorManager");

        jiggleDivisor = 10 - jiggleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
		if (camRig == null)
			camRig = GameObject.FindGameObjectWithTag("Player");
		if (door == null)
			door = GameObject.FindGameObjectWithTag("door");
		if (doorOpen == null)
			doorOpen = GameObject.FindGameObjectWithTag("openDoor");
	//	if (rope == null)
	//		rope = GameObject.FindGameObjectWithTag("rope");
		if (manager == null)
			manager = GameObject.FindGameObjectWithTag("ElevatorManager");
	//	if (hold == null)
	//		hold = GameObject.FindGameObjectWithTag("doorHold");

        doorY = door.transform.position.y;
        pulleyY = pulley.transform.position.y;

        if(!handInRange) {
            //grabbed = false;
        }


        if (grabbed) {
            //Debug.Log("hell yeah get ready to lift");
            lifting = true;
            mirror = (initPulleyY - pulleyY) * scale;
            // Move the door according to the current y position of the controller
            door.transform.position = new Vector3(initX, (door.transform.position.y - hold.transform.position.y) + grabbingHand.transform.position.y, initZ);
           // otherwise Y pos of door and pulley will lerp auto
            
            
            // Once the door reaches a certain height
            // it goes all the way up automatically

            var whichHand = "both";
            //Trigger Haptic pulse
            if (grabbingHand.tag == "leftControl") {
                whichHand = "left";
            }
            else if (grabbingHand.tag == "rightControl") {
                whichHand = "right";
            }
            var strength = (Mathf.Abs(door.transform.position.y - previousHeight) / (highPoint - initY)) * holdingVibrationMax;
            Haptic.rumbleController(0.1f, strength, whichHand);

        } else {
            grabbingUp = false;
            // if door not at a certain point yet
            // it goes down
            if (door.transform.position.y < midPoint && lifting) {
              // while the door position is greater than the y position
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(initX, 1.1f, initZ), 2f * Time.deltaTime);
            }
        }

        if (lifting) {
            // if past certain point
            if (door.transform.position.y > midPoint) {
                door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpen.transform.position, Time.deltaTime);
                //Debug.Log("Attempt to lerP");
                if (door.transform.position.y >= highPoint) {
                    //Debug.Log("I WANT THIS");
                    lifting = false;
                    open = true;
                    //slidingDoor2.openSlidingDoor();

                    //Tell the elevator manager that door is open
                    //manager.GetComponent<ElevatorMovement>().doorOpened();
                }
            }
        }

        if (open) {
            // if door is at the top (open) 
            // if trigger is pressed on rope and it is collided
            if (grabbed) {
                //Debug.Log("hell yeah get ready to close this shit");
                closing = true;
                // Move the door according to the current y position of the controller
                door.transform.position = new Vector3(initX, (door.transform.position.y - rope.transform.position.y) + grabbingHand.transform.position.y, initZ);

            } // condition about where the door goes when the user stops interacting w it in some way?
            else {
                if (door.transform.position.y > midPoint && closing)
                {
                    door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpen.transform.position, 2f * Time.deltaTime);
                }

                grabbingDown = false;
            }
        }

        if (closing)
        {
            if (door.transform.position.y < midPoint)
            {   
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(initX, initY, initZ), Time.deltaTime);
                //Debug.Log("Attempt to lerP down to close");
                //Debug.Log(door.transform.localPosition.y);//flooding console
                if (door.transform.localPosition.y <= 1.17f)  
                {
                    closeDoor();
                }
            }
        }

        if (door.transform.position.y > 1.2f && door.transform.position.y < 3.15)
        {
            if (!playing)
            {
                //stopSFX.PlaySound(transform.position);
                //doorLoopSFX.PlaySound(transform.position);
                playing = true;
            }
        }
        else
        {
            if (playing)
            {
                //startSoundSFX.PlaySound(transform.position);
                GameObject myObject = GameObject.Find("_SFX_"+doorLoopSFX);
                //myObject.GetComponent<SoundGroup>().pingSound();
                playing = false;
            }
        }

        if (jiggling) {
            //Debug.Log("Jiggling");
            if (jiggleTimer < jiggleLength) {
                if (jiggleTimer % jiggleDivisor == 0) {
                    door.transform.position = new Vector3(door.transform.position.x, 1.17f + jiggleStrength * Mathf.Sin(jiggleTimer), door.transform.position.z);
                }
                jiggleTimer++;
            }
            else {
                jiggling = false;
                jiggleTimer = 0;
                door.transform.position = new Vector3(door.transform.position.x, 1.17f, door.transform.position.z);
            }
        }


    } // end of update

    public void attemptGrab(GameObject hand) {
		if (handInRange) {
			nearDoor = true;
			//camRig.GetComponent<DisableGesture>().turnOff ();

			//If Door Open
            /*
			if (manager.GetComponent<ElevatorMovement>().floorPos == Mathf.Round (manager.GetComponent<ElevatorMovement> ().floorPos)) {
                Debug.Log("Grabbing because " + manager.GetComponent<ElevatorMovement>().floorPos);
				grabbed = true;
				grabbingHand = hand;

                previousHeight = door.transform.position.y;

            } else {
				//Trigger the jiggle
				jiggleSound.PlaySound (transform.position);
				jiggling = true;
			}
            */
		} else {
			nearDoor = false;
			//if (!camRig.GetComponent<DisableGesture>().isComponentEnabled ()) {
			//	camRig.GetComponent<DisableGesture>().turnOn ();
			//}
		}
    }

    public void attemptRelease()
    {
        grabbed = false;
    }

    //used in LeverGrab script to stop the lever from moving if door is open
    public bool doorStatus()
    {
        return open;
    }

    public void openDoor() {
        Debug.Log("Open Door");
        closing = false;
        open = true;
        //slidingDoor2.openSlidingDoor();
        door.transform.position = new Vector3(initX, initY, initZ);
    }

    public void closeDoor() {
        Debug.Log("Close Door");
        closing = false;
        open = false;
        //slidingDoor2.closeSlidingDoor();
        door.transform.position = new Vector3(initX, initY, initZ);
    }

}   // eof

