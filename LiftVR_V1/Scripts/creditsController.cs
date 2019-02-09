using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsController : MonoBehaviour {

    public float timeOnFloor;

    private int nextFloor = 1;
    private int maxFloor = 6;

    private doorInteraction liftDoor;
    private ElevatorMovement eleMvmt;

    private bool endStarted;

    private AsyncOperation async;

    // Use this for initialization
    void Start () {
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 5f);

        liftDoor = GameObject.FindGameObjectWithTag("door").GetComponent<doorInteraction>();
        eleMvmt = GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<ElevatorMovement>();

        StartCoroutine(CreditsCycle());
	}
	
	// Update is called once per frame
	void Update () {
        if (eleMvmt.floorPos == nextFloor) {
            if (nextFloor < maxFloor) {
                nextFloor++;
                StartCoroutine(CreditsCycle());
            }
            else if(endStarted == false) {
                //We've reached end of credits
                liftDoor.openDoor();
                endStarted = true;
                StartCoroutine(EndCredits());
            }
        }
    }

    IEnumerator CreditsCycle() {
        yield return new WaitForSeconds(1f);
        //Open door
        liftDoor.openDoor();

        //Wait for timeOnFloor
        yield return new WaitForSeconds(timeOnFloor);

        //Close the door
        liftDoor.closeDoor();

        yield return new WaitForSeconds(3f);

        //Set new target floor
        eleMvmt.newDoorTarget(nextFloor);
    }

    IEnumerator EndCredits() {
        async = SceneManager.LoadSceneAsync("Splash");
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(timeOnFloor);

        liftDoor.closeDoor();

        yield return new WaitForSeconds(3f);

        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 5f);

        yield return new WaitForSeconds(5f);

        async.allowSceneActivation = true;
    }
}
