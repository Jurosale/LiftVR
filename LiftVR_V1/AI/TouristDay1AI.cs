using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristDay1AI : Agent {
    /*
     * Modified GenericAIv2 for first Tourist Interactions
     * 
     * Features:
     * - Will not leave elevator until interaction ends
     * - Different dialogue for leavintg elevator
     */

    private GameObject objYes;
    private GameObject objNo;
    private GameObject gestures;
    private float ty1;
    private float ty2;
    private float ty3;
    private float tn1;
    private float tn2;
    private float tn3;

    // Use this for initialization
    void Start() {
        filename = "1.2Tourist.json";
        Init();
        isEndNode = false;
        isExit = false;
        timer = currentNode.wait;

        //"Yesy" & "No" animation stuff
        objYes = GameObject.FindGameObjectWithTag("tutorialLine");
        objNo = GameObject.FindGameObjectWithTag("tutorialLine2");
        gestures = GameObject.FindGameObjectWithTag("GestureList");
        ty1 = 12.5f;
        ty2 = 2.0f;
        ty3 = 5.0f;
        tn1 = 3.0f;
        tn2 = 3.0f;
        tn3 = 5.0f;
    }

    // Update is called once per frame
    void Update() {
		if (objYes == null)
			objYes = GameObject.FindGameObjectWithTag("tutorialLine");
		if (objNo == null)
			objNo = GameObject.FindGameObjectWithTag("tutorialLine2");
		if (gestures == null)
			gestures = GameObject.FindGameObjectWithTag("GestureList");

        //Calls the "Yes" & "No" animations to play exactly when they're needed 
        if (isExit)
        {
            objYes.GetComponent<Animator>().enabled = false;
            objYes.GetComponent<Animator>().SetBool("startOver", false);
            objNo.GetComponent<Animator>().enabled = false;
            objNo.GetComponent<Animator>().SetBool("startOver", false);
            gestures.GetComponent<Transform>().localScale = new Vector3(.57f, .57f, .57f);
        }
        else if (currentNode.name == "Sign Language")
        {
            ty1 -= Time.deltaTime;
            if (ty1 <= 0.0f) {
                objYes.GetComponent<Animator>().enabled = true;
                objYes.GetComponent<Animator>().SetBool("startOver", true);
                objYes.GetComponent<Animator>().Play("yes");
                objYes.GetComponent<Animator>().SetBool("startOver", false);
                ty1 = 30.0f;
            }
        }

        else if (currentNode.name == "Sign Language...again")
        {
            ty2 -= Time.deltaTime;
            if (ty2 <= 0.0f)
            {
                objYes.GetComponent<Animator>().enabled = true;
                objYes.GetComponent<Animator>().SetBool("startOver", true);
                objYes.GetComponent<Animator>().Play("yes");
                objYes.GetComponent<Animator>().SetBool("startOver", false);
                ty2 = 30.0f;
            }
        }

        else if (currentNode.name == "Sign Language...again...and again")
        {
            ty3 -= Time.deltaTime;
            if (ty3 <= 0.0f)
            {
                objYes.GetComponent<Animator>().enabled = true;
                objYes.GetComponent<Animator>().SetBool("startOver", true);
                objYes.GetComponent<Animator>().Play("yes");
                objYes.GetComponent<Animator>().SetBool("startOver", false);
                ty3 = 12.5f;
            }
        }

        else if (currentNode.name == "Sign Language2")
        {
            tn1 -= Time.deltaTime;
            objYes.GetComponent<Animator>().enabled = false;
            if (tn1 <= 0.0f)
            {
                objNo.GetComponent<Animator>().enabled = true;
                objNo.GetComponent<Animator>().SetBool("startOver", true);
                objNo.GetComponent<Animator>().Play("no");
                objNo.GetComponent<Animator>().SetBool("startOver", false);
                tn1 = 30.0f;
            }
        }

        else if (currentNode.name == "Sign Language2...again")
        {
            tn2 -= Time.deltaTime;
            if (tn2 <= 0.0f)
            {
                objNo.GetComponent<Animator>().enabled = true;
                objNo.GetComponent<Animator>().SetBool("startOver", true);
                objNo.GetComponent<Animator>().Play("no");
                objNo.GetComponent<Animator>().SetBool("startOver", false);
                tn2 = 30.0f;
            }
        }

        else if (currentNode.name == "Sign Language2...again...and again")
        {
            tn3 -= Time.deltaTime;
            if (tn3 <= 0.0f)
            {
                objNo.GetComponent<Animator>().enabled = true;
                objNo.GetComponent<Animator>().SetBool("startOver", true);
                objNo.GetComponent<Animator>().Play("no");
                objNo.GetComponent<Animator>().SetBool("startOver", false);
                tn3 = 12.5f;
            }
        }


        if (timer > 0 && walkedIn) timer -= Time.deltaTime;

        //waiting to walk in
        if (!walkedIn) {
            if (enter()) walkedIn = true;
            else return;
        }

        //start
        if (!doneStart) {
            if (playStart()) doneStart = true;
            else return;
        }

        //setup normal
        if (!doneSetup) {
            if (setupNormal()) doneSetup = true;
            else return;
        }

        //update state
        if (isUpdate) updateState();

        //play normal
        doState();
    }

    //declarations
    private bool walkedIn = false;
    private bool doneStart = false;
    private bool doneSetup = false;
    private bool isUpdate = false;
    private bool isPlayed = false;
    private int state = 0; //0 = story 1 = ready to exit 2 = exit (custom)

    private bool playStart() {
        //once entered will check if door is closed, else it'll repeat "take me to x"
        if (!isDoorOpen()) return true;

        say();

        if (timer <= 0) {
            isPlayed = false;
            //not needed?
            changeMood(currentNode.noResponseChange);
        }

        return false;
    }

    private bool setupNormal() {
        //used to make sure ai finishes start dialogue
        if (timer < currentNode.wait) {
            //set up normal
            currentNode = nodeDict[currentNode.noResponse];
            isPlayed = false;
            return true;
        }

        return false;
    }

    private void updateState() {
        if (!isExit) isPlayed = false;
        switch (state) {
            case 0:
                if (isEndNode) state = 1;
                break;
            case 1:
                if (isDoorOpen()) {
                    if (getFloorNumber() == attributes.goal) {
                        currentNode = nodeDict["End"];
                    }
                    else {
                        currentNode = nodeDict["Detour"];
                    } state = 2;
                }
                break;
            case 2:
                break;
            default:
                Debug.LogError("state out of range: " + state);
                break;
        }
        isUpdate = false;
    }

    private void doState() {
        if(state != 1) say();

        //listen only when dialogue is done
        if (timer > currentNode.wait) {
            resetGesture(); //for hand stuff
            stopGestures();
            return;
        }

        if (currentNode.listen.Count > 0) startGesture(); //for hand stuff

        //get gesture
        string gesture = getGesture();

        //timer in listening range
        switch (state) {
            case 0:
                if (currentNode.name == "Work night" || currentNode.name == "Awkward encounter") {
                    isEndNode = true;
                    isUpdate = true;
                }
                else if (currentNode.listen.Contains(gesture)) {
                    int index = currentNode.listen.IndexOf(gesture);
                    changeMood(currentNode.change[index]);
                    currentNode = nodeDict[currentNode.toNode[index]];
                    isUpdate = true;
                }
                else if (timer <= 0) {
                    changeMood(currentNode.noResponseChange);
                    currentNode = nodeDict[currentNode.noResponse];
                    isUpdate = true;
                }
                break;
            case 1:
                isUpdate = true;
                break;
            case 2:
                if (!isExit) {
                    exit();
                    (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).setMood(name, attributes.mood);
                    isExit = true;
                }
                break;
            default:
                Debug.LogError("state out of range: " + state);
                break;
        }
    }

    private void say() {
        //do not play if dialog already played
        if (isPlayed) return;

        //get mood index
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //get sound file
        string dialogue = currentNode.dialogue[index];

        //play sound
        playDialogue(dialogue);

        //sets up the timer for the next node
        float audioTime = GetComponent<PatronAudio>().patronMouth.clip.length;
        timer = audioTime + currentNode.wait;

        //talking animation
        animate(currentNode.animation[index], audioTime);

        //text bubble
        //bubble.text = currentNode.dialogue[index];

        //update lastSound
        lastSound = dialogue;

        //update isPlayed
        isPlayed = true;
    }
}
