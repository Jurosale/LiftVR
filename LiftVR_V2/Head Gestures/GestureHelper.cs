using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class GestureHelper : MonoBehaviour {

    /// <summary>
    /// Simple print functions to see if gestures are actually working
    /// </summary>
    /// 
    public PlayMakerFSM playerFSM;

    // Use this for initialization
    void Start () {
        playerFSM = GetComponent<PlayMakerFSM>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void yesGesture()
    {
        print("nod");
        playerFSM.SendEvent("Interaction(nod)");
    }

    public void noGesture()
    {
        print("shake head");
        playerFSM.SendEvent("Interaction(shakeHead)");

    }
}
