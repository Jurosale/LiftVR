using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GenericAIv2 : Agent {

    /*
     * New AI that completes a dialogue before moving to the next one.
     * Does not use onNode
     */

    private Regex touristRegex = new Regex(@"Tourist");

    // Use this for initialization
    void Start () {
        Regex regex = new Regex(@"(\.json)$");
        if (!regex.IsMatch(filename)) throw new System.ArgumentNullException("Invalid Filename: " + filename);
        Init();
        isEndNode = false;
        isExit = false;
        timer = currentNode.wait;
        patTimer = attributes.patience;
    }
	
	// Update is called once per frame
	void Update () {

        //timer -  will not start untill walkedIn is true
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
    public bool isPlayed = false;
    public int state = 0; //0 = story 1 = notFloor 2 = exit

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
        if (!isEndNode || state != 0) isPlayed = false;
        switch (state) {
            case 0:
                if (isDoorOpen() && getFloorNumber() != attributes.goal) state = 1;
                else if (isDoorOpen() && getFloorNumber() == attributes.goal) state = 2;
                break;
            case 1:
                if (!isDoorOpen()) state = 0;
                else if (attributes.patience <= 0) state = 2;
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
        //temp state 2 fix
        if (state == 2) {
            if (!isExit) leaveEle();
            return;
        }

        //will not repeat last nodes
        if (!isEndNode || state != 0) say();
        
        //temp onNode
        node n = getNode();

        //just for 3.1Tourist2
        if (touristRegex.IsMatch(name) && n.name == "Divorce") info.flagDivorce();

        //listen only when dialogue is done
        if (timer > n.wait) {
            resetGesture(); //for hand stuff
            stopGestures();
            return;
        }

        if (n.listen.Count > 0) startGesture(); //for hand stuff

        //get gesture
        string gesture = getGesture();

        //timer in listening range
        switch (state) {
            case 0:
                if (n.listen.Contains(gesture)) {
                    int index = n.listen.IndexOf(gesture);
                    changeMood(n.change[index]);
                    currentNode = nodeDict[n.toNode[index]];
                    isUpdate = true;
                } else if (timer <= 0) {
                    changeMood(n.noResponseChange);
                    currentNode = nodeDict[n.noResponse];
                    isUpdate = true;
                } 
                if (n.listen.Count < 1 && n.noResponse == n.name && n.name != notFloorNode.name) isEndNode = true;
                break;
            case 1:
                if (attributes.mood < -3) attributes.patience -= Time.deltaTime;
                if (timer <= 0) {
                    changeMood(n.noResponseChange);
                    isUpdate = true;
                } break;
            case 2:
                if (!isExit) {
                    exit();
                    (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).setMood(name, attributes.mood);
                    isExit = true;
                } break;
            default:
                Debug.LogError("state out of range: " + state);
                break;
        }
    }

    private void leaveEle() {
        if (lastSound != "End") say(true);
        else say();
        stopGestures();
        stopTalking();
        exit();
        info.setMood(name, attributes.mood);
        isExit = true;
    }

    private node getNode() {
        //returns correct state node (onNode without storing)
        switch (state) {
            case 0:
                return currentNode;
            case 1:
                return notFloorNode;
            case 2:
                return endNode;
            default:
                Debug.LogError("state out of range: " + state);
                return null;
        }
    }

    private void say(bool force = false) {
        //do not play if dialog already played
        if (!force && isPlayed) return;

        //get correct dialoge to play (depending on state)
        node n = getNode();

        //get mood index
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //get sound file
        string dialogue = n.dialogue[index];

        //play sound
        playDialogue(dialogue);

        //sets up the timer for the next node
        if (GetComponent<PatronAudio>().patronMouth.clip != null) {
            float audioTime = GetComponent<PatronAudio>().patronMouth.clip.length;

            timer = audioTime + n.wait;

            //talking animation
            if (n.animation[index] == "talk") animate(n.animation[index], audioTime);
            else animate(n.animation[index]);
        }

        //text bubble
        //bubble.text = n.dialogue[index];

        //update lastSound
        lastSound = dialogue;

        //update isPlayed
        isPlayed = true;
    }
}
