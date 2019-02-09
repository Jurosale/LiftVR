using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dateMovement : MonoBehaviour {

    private GameObject targetWaypoint;
    private GameObject hotelManager;
    private GameObject playerHead;

    private GameObject rotateTarget;

    private GameObject leverRotator;

    private GameObject musicSource;

    //For new waypoint system
    private int waypointNumber = 2;

    [Range(0.4f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
    public float rotationSpeed = 5f;

    public bool moving = false;
    public bool rotating = false;
    public bool waiting = false;
    public string state = "";

    [Header("Footsteps")]
    public string footstepSound;
    public float footstepGap;
    private float footstepTimer;

    Animator anim;

    //TIMER
    private float timer = 0;

    public GameObject adultress;


    // Use this for initialization
    void Start() {
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        playerHead = GameObject.FindGameObjectWithTag("MainCamera");
        leverRotator = GameObject.FindGameObjectWithTag("lever");
        musicSource = GameObject.FindGameObjectWithTag("musicControl");
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update() {
		if (playerHead == null)
			playerHead = GameObject.FindGameObjectWithTag("MainCamera");

        //TIMER
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else {
            anim.SetBool("talking", false);
        }

        if (moving) {

            float step = walkSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, step);

            //Make footstep noise
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepGap) {
                footstepSound.PlaySound(transform.position);
                footstepTimer = 0;
            }

            if (transform.position == targetWaypoint.transform.position) {
                //We've reached our destination
                if (state == "entering") {
                    moving = false;

                    // if the NPC hasnt already reached the waypoint in the elevator
                    // aka walked inside
                    if (!anim.GetBool("reachedWaypoint")) {
                        anim.SetBool("reachedWaypoint", true);
                    }
                    else {  // this means they are now going to the waypoint outside of the elevator
                        anim.SetBool("reachedWaypoint2", true);
                    }
                    turnTowardsPlayer();
                }

                else if (state == "leaving") {
                    //Move to a second + n waypoint and then despawn
                    var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
                    //Check if there's another waypoint in the path
                    if (currentFloor != -1) {
                        if (hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor, waypointNumber) != null) {
                            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor, waypointNumber);
                            turnTowardsWaypoint(targetWaypoint);
                            waypointNumber++;
                        }
                    }
                }
            }
        }

        if (rotating) {
            var targetRotation = Quaternion.LookRotation(rotateTarget.transform.position - transform.position);
            targetRotation.x = transform.rotation.x;
            targetRotation.z = transform.rotation.z;
            var str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
    }

    public bool enterElevator() {
        if (hotelManager.GetComponent<FloorManager>().doorOpen == true) {
            //Debug.Log("yes yes yes ");

            anim.SetBool("elevatorHere", true);
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchDateWaypoint();
            moving = true;
            state = "entering";

            return true;
        }
        else {
            return false;
        }
    }

    public bool leaveElevator() {
        if (hotelManager.GetComponent<FloorManager>().doorOpen == true) {
            anim.SetBool("walkOut", true);

            var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint(currentFloor);
            moving = true;

            state = "leaving";

            turnTowardsWaypoint(targetWaypoint);
            return true;
        }
        else {
            return false;
        }
    }

    public void turnTowardsPlayer() {
        rotateTarget = adultress;
        rotating = true;
    }

    public void turnTowardsWaypoint(GameObject waypoint) {
        rotateTarget = waypoint;
        rotating = true;
    }

    public void talk(float time) {
        timer = time;
        anim.SetBool("talking", true);
    }

    public void stopTalking() {
        anim.SetBool("talking", false);
    }

    //Called from AI whenever the mood is changed i indicates by how much
    public void moodChanged(int i) {
        Debug.Log("Mood Changed by: " + i);
        if (i < 0) {
            //Play Angry Animation
            transform.Find("Moods").GetComponent<MoodParticles>().angryAnimation();

        }
        else if (i > 0) {
            //Play Happy Animation
            transform.Find("Moods").GetComponent<MoodParticles>().happyAnimation(i);

        }
        /*
        else {
            //Play Confused Animation
            transform.Find("Moods").GetComponent<MoodParticles>().neutralAnimation();

        }
        */
    }
}