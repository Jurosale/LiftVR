using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GenericAI : Agent {

    void Start()
    {
        Regex regex = new Regex(@"(\.json)$");
        if (!regex.IsMatch(filename)) throw new System.ArgumentNullException("Invalid Filename: " + filename);
        Init();
        isEndNode = false;
        isExit = false;
        timer = currentNode.wait;
        patTimer = attributes.patience;
        Build();
    }

    void Update()
    {
        if (listIndex > 0) timer -= Time.deltaTime;

        if (list[listIndex]())
        {
            listIndex += 1;
            isPlayed = false;
            setup = false;
            resetGesture();

            if (listIndex >= list.Count) listIndex = list.Count - 1;
        }

        if (timer <= onNode.wait) startGesture();
        else stopGestures();
    }

    private void Build()
    {
        list.Add(() => waitStart());
        list.Add(() => playStart());
        list.Add(() => playNormal());
    }

    private delegate bool tEvent();
    private bool isPlayed = false;
    private List<tEvent> list = new List<tEvent>();
    private bool setup = false;
    private int listIndex = 0;
    private int state = 0; //0 = normal 1 = notfloor 2 = exit

    private bool waitStart()
    {
        if (enter()) return true;
        return false;
    }

    private bool playStart()
    {
        if (!isDoorOpen())
        {
            currentNode = nodeDict[currentNode.noResponse];
            onNode = currentNode;
            return true;
        }

        say();

        if (timer <= 0)
        {
            /////////////////////////////timer = currentNode.wait;
            isPlayed = false;
            changeMood(currentNode.noResponseChange);
        }

        return false;
    }

    private bool playNormal()
    {
        if (!setup)
        {
            //////////////////////////////////timer = onNode.wait;
            //stops repeat at an end node
            if (isEndNode && state == 0)
            {
                isPlayed = true;
            }
            else
            {
                say();
            }
            if (onNode.noResponse == onNode.name && onNode != notFloorNode && onNode.listen.Count < 1) isEndNode = true;
            setup = true;
        }

        //node transitions
        int nextState = getNextState();

        //if transition, move and execute
        if (state != nextState)
        {
            Debug.Log("FROM " + state + " TO " + nextState);
            state = nextState;
            return true;
        }

        return doState();
    }

    private int getNextState()
    {
        switch (state)
        {
            case 0:
                if (isDoorOpen() && getFloorNumber() != attributes.goal)
                {
                    onNode = notFloorNode;
                    return 1;
                }
                else if (isDoorOpen() && getFloorNumber() == attributes.goal)
                {
                    onNode = endNode;
                    return 2;
                }
                else return 0;
            case 1:
                if (!isDoorOpen())
                {
                    onNode = currentNode;
                    return 0;
                } else if (attributes.patience <= 0) {
                    onNode = endNode;
                    return 2;
                }
                else return 1;
            case 2:
                return 2;
            default:
                throw new System.IndexOutOfRangeException("STATE MUST BE 0 1 or 2. TRIED TO PASS: " + state);
        }
    }

    private bool doState()
    {
        switch (state)
        {
            case 0:
                if (onNode.listen.Contains(getGesture()))
                {
                    int index = onNode.listen.IndexOf(getGesture());
                    changeMood(onNode.change[index]);
                    currentNode = nodeDict[onNode.toNode[index]];
                    onNode = currentNode;
                    return true;
                }
                else if (timer <= 0)
                {
                    changeMood(onNode.noResponseChange);
                    currentNode = nodeDict[onNode.noResponse];
                    onNode = currentNode;
                    return true;
                }
                return false;
            case 1:
                if (attributes.mood < -3) {
                    attributes.patience -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    changeMood(onNode.noResponseChange);
                    return true;
                }
                return false;
            case 2:
                if (!isExit)
                {
                    stopTalking();
                    exit();
                    (GameObject.FindWithTag("HotelManager").GetComponent(typeof (AIInfo)) as AIInfo).setMood(name, attributes.mood);
                    isExit = true;
                }
                return false;
            default:
                throw new System.IndexOutOfRangeException("STATE MUST BE 0 1 or 2. TRIED TO PASS: " + state);
        }
    }

    private void say()
    {
        if (isPlayed) return;

        node play = onNode;

        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //talking animation
        animate(play.animation[index]);

        //text
        //bubble.text = play.dialogue[index];

        string dialogue = play.dialogue[index];

        GameObject myObject = GameObject.Find("_SFX_" + lastSound);
        if (myObject != null) myObject.GetComponent<SoundGroup>().pingSound();
        //dialogue.PlaySound(transform.position);

        pa.playDialogue(dialogue);

        float audioTime = GameObject.Find("_SFX_" + dialogue).GetComponent<AudioSource>().clip.length;
        timer = audioTime + play.wait;
        lastSound = dialogue;

        isPlayed = true;
    }
}
