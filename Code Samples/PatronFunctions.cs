using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames;
using HutongGames.PlayMaker;

public class PatronFunctions : MonoBehaviour {

    /// <summary>
    /// Attached to all Patron objects
    /// Deals with:
    /// - Holding object spawning/despawn
    /// </summary>
    /// 
    public DeviceInfo deviceL;
    public DeviceInfo deviceR;

    GameObject sdkManager;


    //GameObject item;
    public Transform handHoldL; // TODO: Attach patrons hands to these
    public Transform handHoldR; //  

    ItemInteractions item;      // the thing that they are holding?
                                // is it necessary to keep track of?

    //***************************************************************************************
    string[] interactableTags = new string[1];   // a list of objects that the patron can interact with
                                                 //add the desired tags in Unity editor
                                                 // could give all interactable objects a universial
                                                 // tag as the alternative
    //***************************************************************************************

    GameObject it; //temp game object for storing      
    GameObject playerObject;
    PlayMakerFSM playerFSM;
    public bool alive = true;

    private FloorManager fM;
    private PatronSpawner PS;

    bool gazeTrigger;
    bool objectColliderTrigger;

    GameObject playerHead;
    GameObject elevatorRef;
    GameObject collidedWith;

    FsmGameObject HeadVar;
    FsmGameObject ElevatorVar;
    FsmFloat AudioVar;

    int patronLayer;
    int layerMask;

    float curAcTime = 0, acLength = 2500f; // TODO: get audio clip length

    AudioClip ac;

    float currgazeTime = 0;


    //Moving to Waypoints
    //Reference to the location of the next waypoint
    private GameObject targetWaypoint;

    [Header("Waypoint Movement")]
    [Range(0.5f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
  
    [Header("Footsteps")]
    public string footstepSound;
    public float footstepGap;
    private float footstepTimer;

    // Use this for initialization
    void Start() {
        Debug.Log("start");
        alive = true;
        objectColliderTrigger = false;
        playerObject = GameObject.FindGameObjectWithTag("PlayerManager");
        playerFSM = playerObject.GetComponent<PlayMakerFSM>();
        sdkManager = GameObject.FindGameObjectWithTag("Main");
        playerHead = sdkManager.GetComponent<SDKAdjust>()._camera;
        elevatorRef = GameObject.FindGameObjectWithTag("ElevatorManager");

        patronLayer = LayerMask.NameToLayer("Patron");
        layerMask = (1 << patronLayer);

        HeadVar = FsmVariables.GlobalVariables.FindFsmGameObject("playerHead");     //sets the global variable PlayerHead
        HeadVar.Value = playerHead;

        ElevatorVar = FsmVariables.GlobalVariables.FindFsmGameObject("elevator");   //sets the global variable Elevator
        ElevatorVar.Value = elevatorRef;

        //AudioVar = FsmVariables.GlobalVariables.GetFsmFloat("currentAudio");        //sets the global variable PlayerHead

        fM = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<FloorManager>();
        PS = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<PatronSpawner>();
    }
	// Update is called once per frame
	void Update () {
        if (sdkManager == null) sdkManager = GameObject.FindGameObjectWithTag("Main");
        if (playerHead == null) playerHead = sdkManager.GetComponent<SDKAdjust>()._camera;
        
        

        //if (HeadVar.Value == null) {
        //    HeadVar.Value = playerHead;
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            spawnHoldingObj("Hat", handHoldR.transform);
        }

        //Moving to Waypoint
        if (targetWaypoint != null)
        {
            float step = walkSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, step);
            if (transform.position == targetWaypoint.transform.position)
            {
                if (targetWaypoint.GetComponent<patronWaypoint>().endNode)
                {
                    //despawnPatron();
                    targetWaypoint = null;
                }
                if (targetWaypoint.GetComponent<patronWaypoint>().nextNode != null)
                {
                    targetWaypoint = targetWaypoint.GetComponent<patronWaypoint>().nextNode;
                }
                else
                {
                    targetWaypoint = null;
                }
            }
        }

    }

    void OnDisable()
    {
        if (alive)
        {
            Destroy(this);
            Debug.Log("PrintOnDisable: script was disabled:  " + this);
        }
    }

    //***************************************************************************************
    //requires patrons to have a trigger collider
    // this function is when the patron is RECEIVING an object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractableObject")
        {
            playerFSM.SendEvent("ObjectInteract");
            Debug.Log("other: " + other);
            checkForHoldingObj(other.transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectColliderTrigger = false;
        collidedWith = null;
    }

    //checks for objects with specific tag(s)
    public void checkForHoldingObj(GameObject item)
    {
        //print("CHECKING FOR HOLDNIG OBJ ");
        if (item.tag == "InteractableObject")
        {
            switch (item.name)
            {
                case "Pen":
                    playerFSM.SendEvent("OI[pen]");
                    break;
                case "Hat":
                    print("this the hat probably");
                    break;
                case "Poster_SN":
                    playerFSM.SendEvent("OI[sn]");
                    break;
                case "Poster_AM":
                    playerFSM.SendEvent("OI[am]");
                    break;
                case "Succulent":
                    playerFSM.SendEvent("OI[succ]");
                    break;
                case "Painting":
                    playerFSM.SendEvent("OI[paint]");
                    break;
                default:
                    currentAudio();
                    break;

            }
        }


    }
    //***************************************************************************************

    public void spawnHoldingObj(string Item, Transform pos)
    {
        // all items are in Resources/Items
        it = Instantiate(Resources.Load<GameObject>("Items/" + Item));
        it.transform.position = pos.position;
        print("Attempting to spawn: " + Item + " at this pos: " + it.transform.position);
        it.GetComponent<ItemInteractions>().npcHold = true;
        it.GetComponent<ItemInteractions>().frozen = true;
        // here we can add event sends for when item was spawned
    }

    public void leverTouchCall() {
        if (deviceL.isTouchingLever()) {
            print("l bitch");
            playerFSM.SendEvent("True");
            return;
        }
        if (deviceR.isTouchingLever()) {
            print("r bitch");

            playerFSM.SendEvent("True");
            return;
        }
        print("it shit out");

        return;
    }


    IEnumerator gazeTimer(float timeToGaze)
    {
        gazeTrigger = false;
        //acLength = GetComponent<AudioSource>().clip.length;
        curAcTime = 0f;
        print("starting?");
        Vector3 fwd = playerHead.transform.TransformDirection(Vector3.forward * 10);
        print(layerMask);
        RaycastHit hit;
        //Debug.DrawRay(playerHead.transform.position, fwd, Color.green);
        while (true)
        {
            //print("growing " + curAcTime + " acLength " + acLength);
            Debug.DrawRay(playerHead.transform.position, fwd, Color.green);
            if (Physics.Raycast(playerHead.transform.position, playerHead.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                print("raycast hit: " + hit.transform.name);
                if (currgazeTime < timeToGaze)
                {
                    print("increased");
                    currgazeTime+=Time.deltaTime;
                } else {
                    // GAZE
                    print("you are pointing! ");
                    gazeTrigger = true;
                    currgazeTime = 0;
                    yield break;
                }
            }
            curAcTime+=Time.deltaTime;
            print("now you are doen");
            yield return new WaitForEndOfFrame();
        }
    }


    public void gazeCheck()
    {
        StopCoroutine("gazeTimer");
        if (!gazeTrigger) {
            playerFSM.SendEvent("False");
        } else {
            playerFSM.SendEvent("True");
        }
    }


    public void currentAudio()
    {
        this.gameObject.transform.GetChild(0).transform.Find("Head").GetComponent<AudioSource>().Play();
        //Debug.Log("1: "+this.gameObject.transform.GetChild(0));
        AudioVar = FsmVariables.GlobalVariables.FindFsmFloat("audioLength");
        AudioVar.Value = this.gameObject.transform.GetChild(0).transform.Find("Head").GetComponent<AudioSource>().clip.length;
        return;
    }

    public void updateMemory(string patron, int index)
    {
        //Debug.Log("test");
        switch (patron)
        {
            case "robber":
                GameObject.Find("[VRTK_SDKManager]").GetComponent<PlaymakerPatronFunctions>().robber[index] = true;
                return;
            case "police":
                GameObject.Find("[VRTK_SDKManager]").GetComponent<PlaymakerPatronFunctions>().police[index] = true;
                return;

            default:
                //Debug.Log(patron + " not found (updateMemory)");
                return;
        }//sw
    }

    public void checkMemory(string patron, int index)
    {
        switch (patron)
        {
            case "robber":
                if (GameObject.Find("[VRTK_SDKManager]").GetComponent<PlaymakerPatronFunctions>().robber[index])
                {
                    playerFSM.SendEvent("True");
                    //Debug.Log("robber: " + index + "  T");
                    return;
                }
                playerFSM.SendEvent("False");
                Debug.Log("robber: " + index + "  F");
                return;
            case "police":
                if (GameObject.Find("[VRTK_SDKManager]").GetComponent<PlaymakerPatronFunctions>().police[index])
                {
                    playerFSM.SendEvent("True");
                    //Debug.Log("police: " + index + "  T");
                    return;
                }
                playerFSM.SendEvent("False");
                //Debug.Log("police: " + index + "  F");
                return;

            default:
                //Debug.Log(patron + " not found (checkMemory)");
                return;
        }//sw  
    }

    public void newParent(state.floor targetFloor)
    {
        transform.parent = fM.getReference(targetFloor).transform;
        return;
    }
    public void markForDeath()
    {
        alive = false;
    }

    public void updateSpawn(bool var)
    {
        PS.updateSpawner(var);
    }

    //=======================================
    //Moving to Waypoint Functions
    //=======================================
    public void moveOnWaypoint(string pathName)
    {
        var pathOptions = getFloorWaypoints();

        if (pathOptions == null)
        {
            Debug.LogError("No waypoint path called '" +pathName+"' in the currently active hotel floor");
        }

        foreach (GameObject option in pathOptions)
        {
            //The path which has been requested
            if (option.name == pathName)
            {
                targetWaypoint = option;
            }
        }
    }

    //Check the Hotel Floor to see which Waypoint Paths are Available
    //Defined in the Object of each floor in the hotel
    //Script FloorWarpoints
    //Array floorPaths
    private GameObject[] getFloorWaypoints()
    {
        var hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        GameObject floorObject = null;

        foreach (Transform child in hotelManager.transform)
        {
            if (child.gameObject.activeSelf)
            {
                floorObject = child.gameObject;
            }
        }

        if (floorObject != null)
        {
            return floorObject.GetComponent<FloorWaypoints>().floorPaths;
        }
        else
        {
            return null;
        }
    }
}
