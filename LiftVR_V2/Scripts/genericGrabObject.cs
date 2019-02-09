//collaboration with teammates
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A grabbable object requires a Rigidbody and collider with a trigger
[RequireComponent(typeof(Rigidbody))]
public class genericGrabObject : MonoBehaviour {

    //Is null if hand not in range, assigned to the gameObject colliding with otherwise
    private GameObject rightHand = null;
    private GameObject leftHand = null;

    // Update is called once per frame
    void Update () {
        if (VRInput.getRightTriggerDown() && rightHand != null)
        {
            onGrab(rightHand);
        }
        if (VRInput.getLeftTriggerDown() && leftHand != null)
        {
            onGrab(leftHand);
        }
        if (VRInput.getRightTrigger() && rightHand != null)
        {
            whileGrabbed(rightHand);
        }
        if (VRInput.getLeftTrigger() && leftHand != null)
        {
            whileGrabbed(leftHand);
        }
        if (VRInput.getRightTriggerRelease() && rightHand != null)
        {
            onRelease(rightHand);
        }
        if (VRInput.getLeftTriggerRelease() && leftHand != null)
        {
            onRelease(leftHand);
        }
    }

    public virtual void onGrab(GameObject grabber)
    {

    }

    public virtual void whileGrabbed(GameObject grabber)
    {        

    }

    //Called for either:
    //1) Trigger released while holding an object
    //2) Hand leaves range of object | This is handled onTriggerExit
    public virtual void onRelease(GameObject grabber)
    {

    }
    
    //this and below are my code contributions
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "grabPointL")
        {
            leftHand = other.gameObject;
        }
        if(other.gameObject.tag == "grabPointR")
        {
            rightHand = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "grabPointL")
        {
            if(leftHand != null)
            {
                onRelease(leftHand);
            }
            leftHand = null;
        }
        if(other.gameObject.tag == "grabPointR")
        {
            if (rightHand != null)
            {
                onRelease(rightHand);
            }
            rightHand = null;
        }
    }
}
