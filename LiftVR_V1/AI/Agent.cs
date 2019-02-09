using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for speech bubble
using UnityEngine.UI;
//for gestures
using Edwon.VR.Gesture;

public abstract class Agent : MonoBehaviour {

    //example.json
    public string filename;

    //agent data
    protected agentAttr attributes;
    protected Dictionary<string, node> nodeDict = new Dictionary<string, node>();

    //node data
    protected node onNode; //only used in GenericAIv1 NOT v2
    [SerializeField]
    protected node currentNode;
    protected node notFloorNode;
    protected node endNode;

    //util
    protected GestureList gl;
    protected Text bubble;
    [SerializeField]
    protected float timer;
    protected PatronMovement pm;
    protected bool isStarted;
    protected FloorManager fm;
    protected bool isExit;
    protected string lastSound;
    [SerializeField]
    protected bool isEndNode;
    protected StateFetch sf;
    protected float patTimer;
    private bool isSetMood = false;
    private short mood;
    protected bool isWaitingForGesture;
    protected PatronAudio pa;
    protected AIInfo info;

    //useful stuff
    protected string getGesture() { return gl.getGesture(); }
    protected void resetGesture() { gl.resetGesture(); }
    protected bool isDoorOpen() { return fm.doorOpen; }
    protected bool enter() { return pm.enterElevator(); }
    protected bool exit() { return pm.leaveElevator(); }
    protected int getFloorNumber() { return fm.floorPos; }
    protected bool isNearLever() { return sf.nearLever(); }
    protected bool isNearDoor() { return sf.nearDoor(); }
    protected void stopTalking() { pm.stopTalking(); } //is needed?
    protected void startGesture() { sf.waitingForGesture(); }
    protected void stopGestures() { sf.stopWaitingGesture(); }
    protected void moodParticles(int i) { pm.moodChanged(i); }
    protected void playDialogue(string s) { pa.playDialogue(s); }

    //animation
    protected void animate(string s, float time = -1) {
        if (time > 0) {
            pm.talk(time);
        } else if (s != "") {
            pm.Invoke(s, 0);
        }
    }
    
    protected virtual void Init()
    {
        //get json data
        JsonParser jp = new JsonParser();
        jsonClass jsondata = jp.parse(filename);
        attributes = jsondata.agentAttr;
        for (int i = 0; i < jsondata.nodes.Count; i++)
        {
            nodeDict.Add(jsondata.nodes[i].name, jsondata.nodes[i]);
        }

        //get util
        gl = GameObject.FindWithTag("Player").GetComponent<GestureList>();
        bubble = GetComponentInChildren<Text>();
        pm = GetComponent<PatronMovement>();
        fm = GameObject.FindWithTag("HotelManager").GetComponent<FloorManager>();
        sf = GameObject.FindWithTag("Player").GetComponent<StateFetch>();
        pa = GetComponent<PatronAudio>();
        info = (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo);

        //key nodes
        currentNode = nodeDict["Start"];
        notFloorNode = nodeDict["notFloor"];
        endNode = nodeDict["End"];
        onNode = currentNode;

        //mood setting
        if (isSetMood) attributes.mood = mood;
    }

    //decorative stuff
    protected delegate bool decorative();

    protected bool selector(List<decorative> ld)
    {
        for (int i = 0; i < ld.Count; i++)
        {
            if (ld[i]()) return true;
        }
        return false;
    }

    protected bool sequence(List<decorative> ld)
    {
        for (int i = 0; i < ld.Count; i++)
        {
            if (!ld[i]()) return false;
        }
        return true;
    }

    //agent util

    protected void changeMood(short i)
    {
        attributes.mood += i;

        if (attributes.mood > 10) attributes.mood = 10;
        if (attributes.mood < -10) attributes.mood = -10;

        moodParticles(i);
    }

    public void setMood(short i) {
        mood = i;
        isSetMood = true;
    }

    public void setFilename(string s) {
        filename = s;
    }

    public int getGoal() {
        return attributes.goal;
    }
}
