using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMovement : MonoBehaviour {

	private GameObject targetWaypoint;
	private GameObject hotelManager;
	private GameObject playerHead;

	private GameObject rotateTarget;

	private GameObject leverRotator;

	private GameObject musicSource;

	private GameObject contConf;
	private GameObject Salute;
	private GameObject Rude;

    private GameObject CCpaper;
    private GameObject SRpaper;

    //For new waypoint system
    private int waypointNumber = 2;

	[Range(0.5f, 5)]
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
	private float adultressTimer = -99;
	private bool talking = false;
	private int listPoint = 0;

    private bool nextSpawned = false;

	//Adultress Only
	private GameObject dateBoi;


	// Use this for initialization
	void Start () {
		hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
		playerHead = GameObject.FindGameObjectWithTag("MainCamera");
		leverRotator = GameObject.FindGameObjectWithTag("lever");
		musicSource = GameObject.FindGameObjectWithTag("musicControl");
		anim = GetComponent<Animator> ();
		contConf = GameObject.FindGameObjectWithTag("tutorialLine3");
		Salute = GameObject.FindGameObjectWithTag("tutorialLine3");
		Rude = GameObject.FindGameObjectWithTag("tutorialLine3");
	}


	// Update is called once per frame
	void Update () {

		if (hotelManager == null)
			hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
		if (playerHead == null)
			playerHead = GameObject.FindGameObjectWithTag("MainCamera");
		if (leverRotator == null)
			leverRotator = GameObject.FindGameObjectWithTag("lever");
		if (musicSource == null)
			musicSource = GameObject.FindGameObjectWithTag("musicControl");
		if (anim == null)
			anim = GetComponent<Animator> ();
		if (leverRotator == null)
			contConf = GameObject.FindGameObjectWithTag("tutorialLine3");
            CCpaper = GameObject.FindGameObjectWithTag("ContConf");
        if (leverRotator == null)
			Salute = GameObject.FindGameObjectWithTag("tutorialLine3");
		if (leverRotator == null)
			Rude = GameObject.FindGameObjectWithTag("tutorialLine3");
            SRpaper = GameObject.FindGameObjectWithTag("SalRud");

        //TIMER
        if (timer > 0)
		{
			timer -= Time.deltaTime;
		} else
		{
			if (gameObject.tag != "Adultress" || dateBoi == null)
				anim.SetBool ("talking", false);
			///else if (gameObject.name == "Business1(Clone)" && hotelManager.GetComponent<DayManager> ().day == 2) {
				///anim.SetBool ("phoning", false);
			//}
				
		}

		//Adultress Timer
		if (gameObject.tag == "Adultress" && dateBoi.gameObject.name == "Date1" && talking == true) {
			if (adultressTimer > 0) {
				adultressTimer -= Time.deltaTime;
			}
			else if(adultressTimer != -99) {
				//Check for next animation in the list
				//Disable the one who was currently talking and enable the next

				anim.SetBool("talking", false);
				dateBoi.GetComponent<dateMovement>().stopTalking();

				var animationList = hotelManager.GetComponent<talkAnimationList>().fetchAnimationList(GetComponent<PatronAudio>().currentAudio);
				if (animationList.Count > listPoint) {
					var animation = animationList[listPoint];
					if (animation.adultress == "Adultress") {
						adultressTimer = animation.timeTilNext;
						anim.SetBool("talking", true);
					}
					else if (animation.adultress == "Date") {
						adultressTimer = animation.timeTilNext;
						dateBoi.GetComponent<dateMovement>().talk(animation.timeTilNext);
					}
					Debug.Log("List point one increased");
					listPoint++;
				}
			}
		}

		if (moving)
		{
            //If the floor has changed since getting off the elevator
            if (targetWaypoint.transform.parent.gameObject.activeSelf == false) {
                spawnNextPatron();
                despawnPatron();
                return;
            }

			//If the door has closed on the patron
			if(hotelManager.GetComponent<FloorManager>().doorOpen == false && state == "leaving") {
				spawnNextPatron();
				return;
			}

			float step = walkSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, step);

			//Make footstep noise
			footstepTimer += Time.deltaTime;
			if(footstepTimer >= footstepGap) {
                if (hotelManager.GetComponent<FloorManager>().doorOpen == true) {
                    footstepSound.PlaySound(transform.position);
                }
				footstepTimer = 0;
			}

			if (transform.position == targetWaypoint.transform.position)
			{
				//We've reached our destination
				if(state == "entering"){
					moving = false;

					// if the NPC hasnt already reached the waypoint in the elevator
					// aka walked inside
					if (!anim.GetBool ("reachedWaypoint")) {
						anim.SetBool("reachedWaypoint", true);
					} else {	// this means they are now going to the waypoint outside of the elevator
						anim.SetBool("reachedWaypoint2", true);
					}
					turnTowardsPlayer();
				}

				else if (state == "leaving")
				{
					//Move to a second + n waypoint and then despawn
					var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
					//Check if there's another waypoint in the path
					if (currentFloor != -1) {
						if (hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor, waypointNumber) != null) {
							targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor, waypointNumber);
							turnTowardsWaypoint(targetWaypoint);
							waypointNumber++;
						}
						else {
                            spawnNextPatron();
							despawnPatron();
						}
					}
				}
			}
		}

		if (rotating)
		{
			var targetRotation = Quaternion.LookRotation(rotateTarget.transform.position - transform.position);
			targetRotation.x = transform.rotation.x;
			targetRotation.z = transform.rotation.z;
			var str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

			if(transform.rotation == targetRotation)
			{
				//We're looking at player, stop looking

				//rotating = false;
			}
		}
	}

	public bool enterElevator()
	{
		if (hotelManager.GetComponent<FloorManager>().doorOpen == true)
		{
            //Tell the manager a patron is in the elevator
            hotelManager.GetComponent<DayManager>().patronPresent = true;

			//If Adultress on day 1 or 2, bring their date boi too
			if(gameObject.tag == "Adultress") {
				var date1 = transform.Find("Date1");
				var date2 = transform.Find("Date2");
				if(date1.gameObject.activeSelf == true) {
					date1.GetComponent<dateMovement>().enterElevator();
					dateBoi = date1.gameObject;
					date1.transform.parent = null;
				}
				else if(date2.gameObject.activeSelf == true) {
					date2.GetComponent<dateMovement>().enterElevator();
					dateBoi = date2.gameObject;
					date2.transform.parent = null;
				}
			}

			//Turn off light here
			leverRotator.GetComponent<patronWaiting>().lightOff();
			// turn on floor goal light here
			leverRotator.GetComponent<patronWaiting>().lightGoal(this.gameObject.GetComponent<Agent>().getGoal());    // goal of json 


			//Turn on theme music
			var musicID = 3;
			if(gameObject.tag == "Boss") { musicID = 1; }
			if(gameObject.tag == "Business") { musicID = 2; }
			if(gameObject.tag == "Tourist") { musicID = 3; }
			if (gameObject.tag == "Server") { musicID = 4; }
			if (gameObject.tag == "Adultress") { musicID = 5; }
			if (gameObject.tag == "Artist") { musicID = 6; }
			musicSource.GetComponent<musicController>().playCharacterTheme(musicID);

			anim.SetBool ("elevatorHere", true);
			targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchElevatorWaypoint();
			moving = true;
			state = "entering";

			transform.parent = null;

			return true;
		}
		else
		{
			return false;
		}

	}

	public bool leaveElevator()
	{
		if (hotelManager.GetComponent<FloorManager>().doorOpen == true)
		{
            //Tell the manager a patron is in the elevator
            hotelManager.GetComponent<DayManager>().patronPresent = false;

            //If Adultress on day 1 or 2, bring their date boi too
            if (gameObject.tag == "Adultress") {
				dateBoi.GetComponent<dateMovement>().leaveElevator();

				// if it's day 3 then she drops the ring boys!!!!!
				if (hotelManager.GetComponent<DayManager>().day == 3) {

				}
			}

			anim.SetBool("walkOut", true);

			// turn off light goal light
			leverRotator.GetComponent<patronWaiting>().lightOff();

			var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
			targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint(currentFloor);
			moving = true;

			state = "leaving";


			turnTowardsWaypoint(targetWaypoint);

			//transform.parent = hotelManager.GetComponent<FloorManager>().floors[currentFloor].transform;
			// GetComponent<Animator>().SetBool("reachedWaypoint", false);

			//Resume the elevator music
			musicSource.GetComponent<musicController>().characterExit();

			return true;
		}
		else
		{
			return false;
		}
	}

	public void despawnPatron() { 
		Debug.Log("Called despawn");
		if (gameObject.tag == "Adultress"){
			Destroy(dateBoi.gameObject);
		}
		Destroy(this.gameObject);
	}

    public void spawnNextPatron() {
        if (nextSpawned == false) {
            hotelManager.GetComponent<DayManager>().nextPatron();
            nextSpawned = true;
        }
        
    }

	public void wait()
	{
		waiting = true;
	}

	public void turnTowardsPlayer()
	{
		if (gameObject.tag == "Adultress") {
			rotateTarget = dateBoi;
		}
		else {
			rotateTarget = playerHead;
		}
		rotating = true;
	}

	public void turnTowardsWaypoint(GameObject waypoint)
	{
		rotateTarget = waypoint;
		rotating = true;
	}

	//For the remaining gesture animations
	public void confused() {
		contConf.GetComponent<TutorialLines>().enabled = true;
        CCpaper.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
	}

	public void notConfused() {
		contConf.GetComponent<TutorialLines>().enabled = false;
        CCpaper.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }

	public void salute() {
		Rude.GetComponent<TutorialRude>().enabled = false;
		Salute.GetComponent<TutorialSalute>().enabled = true;
        SRpaper.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }

	public void notSalute() {
		Salute.GetComponent<TutorialSalute>().enabled = false;
		Rude.GetComponent<TutorialRude>().enabled = false;
        SRpaper.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }

	public void rude() {
		Salute.GetComponent<TutorialSalute>().enabled = false;
		Rude.GetComponent<TutorialRude>().enabled = true;
        SRpaper.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }

	public void talk(float time) {
		if (gameObject.tag == "Adultress" && dateBoi.gameObject.name == "Date1")
		{
			listPoint = 0;
			talking = true;
			adultressTimer = 0;
		//} else if (gameObject.name == "Business1(Clone)" && hotelManager.GetComponent<DayManager>().day == 3) {
		//	anim.SetBool("phoning", true);

		} else {
			timer = time;
			Debug.LogWarning(time);

			anim.SetBool("talking", true);
		}
		contConf.GetComponent<TutorialLines>().enabled = false;
		Salute.GetComponent<TutorialSalute>().enabled = false;
		Rude.GetComponent<TutorialRude>().enabled = false;
	}

	public void stopTalking() {
		//if (gameObject.name == "Business1(Clone)" && hotelManager.GetComponent<DayManager>().day == 2) {
		//	anim.SetBool("phoning", false);
	//	}
		anim.SetBool("talking", false);
		if (gameObject.tag == "Adultress" && dateBoi.gameObject.name == "Date1")
		{
			dateBoi.GetComponent<dateMovement>().stopTalking();
			talking = false;
		}
	}

	public void ringDrop() {
		// GameObject.

	}


	//Called from AI whenever the mood is changed i indicates by how much
	public void moodChanged(int i) {
		Debug.Log("Mood Changed by: " + i);
		if(i < 0) {
			//Play Angry Animation
			transform.Find("Moods").GetComponent<MoodParticles>().angryAnimation();
		}
		else if(i > 0) {
			//Play Happy Animation
			transform.Find("Moods").GetComponent<MoodParticles>().happyAnimation(i);
		}
		if (gameObject.tag == "Adultress" && dateBoi.gameObject.name == "Date1") {
			dateBoi.GetComponent<dateMovement>().moodChanged(i);
		}
		/*
        else {
            //Play Confused Animation
            transform.Find("Moods").GetComponent<MoodParticles>().neutralAnimation();

        }
        */
	}
	/*
    IEnumerator StopParticle(float time, string ParticleName) {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        transform.Find(ParticleName).gameObject.SetActive(false);
    }
    */
}
