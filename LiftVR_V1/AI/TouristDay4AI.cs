using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristDay4AI : Agent {

    // Use this for initialization
    void Start() {
        filename = "4.3Tourist.json";
        Init();
        patTimer = attributes.patience;
    }

    // Update is called once per frame
    void Update() {
        if (timer > 0 && isEntered) timer -= Time.deltaTime;

        if (!isEntered) {
            if (enter()) {
                isEntered = true;
            }
        }
        else if (!isStart) {
            say(currentNode);
            if (!isDoorOpen()) {
                if (attributes.mood > 3) {
                    currentNode = nodeDict["Delusional"];
                    delusional = true;
                } else currentNode = nodeDict["Facing one's fears"];
                isStart = true;
            }
            else if (timer <= 0) {
                isPlayed = false;
            }
        }
        else if (!isSetup) {
            if (timer < nodeDict["Start"].wait) {
                isPlayed = false;
                isSetup = true;
                timer = 0;
            }
        }
        else doState();
    }

    //declarations
    private bool isEntered = false;
    private bool isStart = false;
    private bool isSetup = false;
    private bool isPlayed = false;
    private bool flag = false;
    private bool delusional = false;
    

    private void doState() {

        node n = currentNode;

        if (timer > n.wait) {
            resetGesture();
            stopGestures();
            return;
        }

        if (n.listen.Count > 0) startGesture();

        string gesture = getGesture();

        switch (currentNode.name) {
            case "Delusional":
            case "Second visit":
            case "Life to the fullest":
                playDelusionalArc();
                break;
            case "Ideal attendant":
                playDelusionalEnd();
                break;
            case "Facing one's fears":
                playHusbandStart();
                break;
            case "Getting Divorced":
                playHusbandDivorce();
                break;
            case "Confrontations":
            case "What now?":
            case "When the moment is right":
                playHusbandNormal();
                break;
            case "Split decision":
                playHusbandEnd(nodeDict["Somber goodbye"], nodeDict["Losing time 2"]);
                break;
            case "Run away":
                playHusbandEnd(nodeDict["Never looking back"], nodeDict["Never looking back 2"]);
                break;
            case "Sooner the better":
                playConfrontationEnd(nodeDict["Reunited"], nodeDict["Losing time"]);
                break;
            case "Private manners":
                playConfrontationEnd(nodeDict["Somber goodbye"], nodeDict["Losing time 2"]);
                break;
            default:
                break;
        }
    }

    private void playDelusionalArc() {
        say(currentNode);
        if (timer <= 0) {
            currentNode = nodeDict[currentNode.noResponse];
            isPlayed = false;
        }
    }

    private void playDelusionalEnd() {
        if (isDoorOpen()) {
            if (getFloorNumber() == attributes.goal) {
                walkingOut();
            } else {
                if (lastSound == notFloorNode.name) {
                    if (timer <= 0) say(notFloorNode, true);
                } else say(notFloorNode, true);
            }
        }
    }

    private void playHusbandStart() {
        say(currentNode);
        if  (info.getDivorce()) {
            currentNode = nodeDict["What now?"];
            flag = true;
        } else currentNode = nodeDict["Getting Divorced"];
    }

    private void playHusbandDivorce() {
        if (timer <= 0) {
            say(currentNode, true);
            currentNode = nodeDict["What now?"];
            flag = true;
        }
    }

    private void playHusbandNormal() {
        if (flag) {
            stopGestures();
            if (timer <= 0) {
                flag = false;
                isPlayed = false;
            }
            else return;
        }
        say(currentNode);
        if (timer <= nodeDict[lastSound].wait) {
            if (isDoorOpen()) {
                if (getFloorNumber() == attributes.goal) {
                    currentNode = endNode;
                } else {
                    if (lastSound == "notFloor") {
                        if (timer <= 0) say(notFloorNode, true);
                    } else say(notFloorNode, true);
                }
            } else {
                if (lastSound == "notFloor") {
                    say(currentNode, true);
                } else {
                    string gesture = getGesture();
                    if (currentNode.listen.Contains(gesture)) {
                        int index = currentNode.listen.IndexOf(gesture);
                        changeMood(currentNode.change[index]);
                        currentNode = nodeDict[currentNode.toNode[index]];
                        isPlayed = false;
                    } else if (timer <= 0) {
                        changeMood(currentNode.noResponseChange);
                        currentNode = nodeDict[currentNode.noResponse];
                        isPlayed = false;
                    }
                }
            }
        } else {
            resetGesture();
        }
    }

    private void playHusbandEnd(node right, node wrong) {
        say(currentNode);
        if (isDoorOpen()) {
            if (getFloorNumber() == attributes.goal) currentNode = right;
            else  currentNode = wrong;
            walkingOut();
        }
    }

    private void playConfrontationEnd(node right, node ball) {
        say(currentNode);
        if (isDoorOpen()) {
            if (getFloorNumber() == attributes.goal) currentNode = right;
            else if (getFloorNumber() == 2) currentNode = ball;
            else currentNode = endNode;
            walkingOut();
        }
    }

    private void walkingOut() {
        say(currentNode, true);
        stopGestures();
        stopTalking();
        exit();
        currentNode.name = "";
        info.setMood(name, attributes.mood);
    }

    private void say(node n, bool force = false) {
        //do not play if dialog already played -- force will play no matter what
        if (!force && isPlayed) return;

        //get mood index
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //get sound file
        string dialogue = n.dialogue[index];

        //play sound
        playDialogue(dialogue);

        //sets up the timer for the next node
        float audioTime = GetComponent<PatronAudio>().patronMouth.clip.length;
        timer = audioTime + n.wait;

        //talking animation
        animate(n.animation[index], audioTime);

        //text bubble
        //bubble.text = n.dialogue[index];

        //update lastSound
        lastSound = n.name;

        //update isPlayed
        isPlayed = true;
    }
}
