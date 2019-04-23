using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using HutongGames.PlayMaker;

public class ItemInteractions : MonoBehaviour {

    public enum Items {
        Hat,
        ID,
        Flyer,
        Painting,
        Plant
    };
    
    public Items thisItem;

    string itemName;
    int objEvent = 1;

    bool itemGrabbed = false;
    bool objCollide = false;
    public bool npcHold = false;
    public bool frozen = true;

    public PlayMakerFSM playerFSM;

    // Use this for initialization
    void Start () {
        itemGrabbed = GetComponent<VRTK_InteractableObject>().IsGrabbed();
    }

    // Update is called once per frame
    void Update () {

        itemGrabbed = GetComponent<VRTK_InteractableObject>().IsGrabbed();

        if (itemGrabbed && objCollide && npcHold) {
            //sendGrabEvent(objEvent);
            npcHold = false;
        }

        if (frozen) {
            //when it is spawned it is usually frozen in the persons hand
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //gravity off?
        } 
            //

     

    }


    //void sendGrabEvent(int i) {
    //    playerFSM.SendEvent("ObjectInteract(" + i + ")");
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.name == "HandR" || other.name == "HandL") {
            objCollide = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "HandR" || other.name == "HandL") {
            objCollide = false;
        }
    }
}
