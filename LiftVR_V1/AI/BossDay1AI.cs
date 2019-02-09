using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDay1AI : Agent {

    // Use this for initialization
    void Start() {
        filename = "1.1Boss.json";
        Init();
        timer = currentNode.wait;
    }

    // Update is called once per frame
    void Update() {

        //timer -  will not start untill walkedIn is true
        if (timer > 0) timer -= Time.deltaTime;

        //play normal
        doState();
    }

    //declarations
    private bool isPlayed = false;
    private bool flag = false;
    private int state = 0; //0 = outside 1 = wait to close door 2 = exposition 3 = wait to leave 4 = leave

    private void doState() {
        switch (state) {
            case 0:
                walkingIn();
                break;
            case 1:
                closingDoor();
                break;
            case 2:
                exposition();
                break;
            case 3:
                readyToLeave();
                break;
            case 4:
                walkingOut();
                break;
            default:
                break;
        }
    }

    public void walkingIn() {
        if (flag) {
            if (timer <= 0) {
                currentNode = nodeDict["First order"];
                isPlayed = false;
                state = 1;
                flag = false;
            } return;
        }
        say();
        if (enter()) {
            if (timer > currentNode.wait) {
                timer -= currentNode.wait;
            } else timer = 0;
            flag = true;
        }
        else if (timer <= 0) {
            currentNode = nodeDict["Still waiting"];
            isPlayed = false;
        }
    }

    public void closingDoor() {
        if (flag) {
            if (timer <=0) {
                currentNode = nodeDict["Day to day operations"];
                isPlayed = false;
                state = 2;
                flag = false;
            } return;
        }
        say();
        if (!isDoorOpen()) {
            if (timer > currentNode.wait) {
                timer -= currentNode.wait;
            } else timer = 0;
            flag = true;
            return;
        }
        if (timer <= 0) {
            currentNode = nodeDict["Ancient mechanism"];
            if (isNearLever()) isPlayed = false;
        }
    }

    public void exposition() {
        say();
        if (timer <= 0) {
            currentNode = nodeDict[currentNode.noResponse];
            isPlayed = false;
        }
        if (currentNode.name == "Extra") {
            state = 3;
        }
    }

    public void readyToLeave() {
        if (timer < currentNode.wait) {
            if (isDoorOpen()) {
                if (getFloorNumber() == attributes.goal) state = 4;
                else if (currentNode.name == "notFloor" && timer <= 0) {
                    isPlayed = false;
                    say();
                } else if (currentNode.name != "notFloor") {
                    currentNode = notFloorNode;
                    isPlayed = false;
                    say();
                }
            } else if (timer <= 0) {
                currentNode = nodeDict["Extra"];
                isPlayed = false;
                say();
            }
        }
    }

    public void walkingOut() {
        currentNode = nodeDict["End"];
        isPlayed = false;
        say();
        stopTalking();
        exit();
        info.setMood(name, attributes.mood);
        state = -1;
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
