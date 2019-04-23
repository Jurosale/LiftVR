using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DeviceInfo : MonoBehaviour {

    GameObject playerObject;
    PlayMakerFSM playerFSM;

    VRTK_ControllerEvents e;

    [SerializeField]
    GameObject hand, point;

    public bool trigger = false, triggerRelease = false;

    bool leverTouch = false;

    float counter = 0f;

    int patronLayer;
    int layerMask;

    LayerMask lm;


    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("PlayerManager");
        playerFSM = playerObject.GetComponent<PlayMakerFSM>();

        patronLayer = LayerMask.NameToLayer("Patron");
        layerMask = (1 << patronLayer);

        e = GetComponent<VRTK_ControllerEvents>();
        hand = transform.GetChild(0).gameObject;
        point = transform.GetChild(1).gameObject;
        e.TriggerReleased += triggerReleased;
        e.TriggerPressed += triggerPressed;

    }

    // Update is called once per frame
    void Update () {


        if (e.triggerPressed) {
            BeginPoint();
        }
        else {
            EndPoint();
        }

    }
    // ON AWAKE
    void OnAwake()
    {
        //do something
    }

    private void OnEnable()
    {
        //do something        
    }

    private void OnDisable()
    {
        //do something
    }

    void BeginPoint() {
        // visual change
        point.SetActive(true);
        hand.SetActive(false);
        // mechanics change
    }

    void EndPoint() {
        point.SetActive(false);
        hand.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Rotator") {
            leverTouch = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "Rotator") {
            leverTouch = false;
        }
    }

    public bool isTouchingLever() {
        return leverTouch;
    }

    void triggerReleased(object sender, ControllerInteractionEventArgs e) {
        triggerRelease = true;
        trigger = false;
    }

    void triggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        triggerRelease = false;
        trigger = true;
    }


    IEnumerator gazeTimer()
    {
        gazeTrigger = false;
        Vector3 fwd = transform.TransformDirection(Vector3.forward*10);
        Debug.DrawRay(transform.position, fwd, Color.green);
        if (Physics.Raycast(transform.position, fwd, Mathf.Infinity, layerMask)) {
            
            if (counter < lookedAt)
            {
                counter++;
            }
            else
            {
                // GAZE
                gazeTrigger = true;
                counter = 0;
                yield break;
            }
        }

    }


    public void HandPoint()
    {
        float lookedAt = 1f;
        RaycastHit objHit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd, Color.green);
        counter = 0;
        if (Physics.Raycast(transform.position, fwd, out objHit, Mathf.Infinity, patronLayer))
        {
            print(objHit);

            if (counter < lookedAt)
            {
                counter++;
            }
            else
            {
                // GAZE
            }

        }
    }

}
