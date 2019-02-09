using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessDay4AI : Agent {

	// Use this for initialization
	void Start () {
        filename = "4.2Businessman.json";
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0 && isEntered) timer -= Time.deltaTime;

        if (!isEntered) {
            if (enter()) {
                isEntered = true;
            }
        } else if (!isStart) {
            say();
            if (!isDoorOpen()) {
                currentNode = nodeDict["Start1"];
                isStart = true;
            } else if (timer <= 0) {
                isPlayed = false;
            }
        } else if (!isSetup) {
            if (timer < nodeDict["Start"].wait) {
                isPlayed = false;
                isSetup = true;
            }
        }
        else doState();
	}

    //declarations
    private bool isEntered = false;
    private bool isStart = false;
    private bool isSetup = false;
    private bool isPlayed = false;

    private void doState() {
        if (!isEndNode) say();

        node n = currentNode;

        if (timer > n.wait) {
            resetGesture();
            stopGestures();
            return;
        }

        if (n.listen.Count > 0) startGesture();

        string gesture = getGesture();

        switch (currentNode.name) {
            case "Start1": case "Home Value": case "Hard workers":
                normal(n, gesture);
                break;
            case "Missed opportunity": case "Perfect for the business":
            case "notFloor": case "Server": case "Artist": case "Other":
                leaveElevator();
                break;
            case "No Regrets": case "One Sided": case "Parasites":
                waitForFloor("Missed opportunity", "notFloor");
                break;
            case "Cutthroat":
                waitForFloor("Perfect for the business", "notFloor");
                break;
            case "Diamond in the rough":
                pickFloor();
                break;
            default:
                break;
        }
    }

    private void normal(node n, string gesture) {
        if (n.listen.Contains(gesture)) {
            int index = n.listen.IndexOf(gesture);
            changeMood(n.change[index]);
            currentNode = nodeDict[n.toNode[index]];
            isPlayed = false;
        }
        else if (timer <= 0) {
            changeMood(n.noResponseChange);
            currentNode = nodeDict[n.noResponse];
            isPlayed = false;
        }
    }

    private void waitForFloor(string right, string wrong) {
        //true if door it open else false;
        if (isDoorOpen()) {
            if (getFloorNumber() == attributes.goal) currentNode = nodeDict[right];
            else currentNode = nodeDict[wrong];
            isPlayed = false;
        }
        else if (timer <= 0) isPlayed = false;
    }

    private void leaveElevator() {
        if (!isExit) {
            stopTalking();
            exit();
            (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).setMood(name, attributes.mood);
            isExit = true;
            if (currentNode.name == "Server") info.setBusinessPick(0);
            else if (currentNode.name == "Artist") info.setBusinessPick(1);
            else info.setBusinessPick(2);
        }
    }
    
    private void pickFloor() {
        if (isDoorOpen()) {
            int i = getFloorNumber();
            if (i == 6) currentNode = nodeDict["Server"];
            else if (i == 2) currentNode = nodeDict["Artist"];
            else currentNode = nodeDict["Other"];
            isPlayed = false;
        } else if (timer <= 0) isPlayed = false;
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
