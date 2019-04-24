using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A grabbable object requires a Rigidbody and collider with a trigger
[RequireComponent(typeof(Rigidbody))]
public abstract class genericGrabObject : MonoBehaviour
{

    //Is null if hand not in range, assigned to the gameObject colliding with otherwise
    protected GameObject rightHand = null;
    protected GameObject leftHand = null;

    //gives every grabbable object different grab states and implementations
    protected abstract void onGrab(GameObject grabber);
    protected abstract void whileGrabbed(GameObject grabber);
    protected abstract void onRelease(GameObject grabber);

    // Update is called once per frame
    void Update()
    {
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

    //detects when a player's hand is within range of object
    protected void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "grabPointL")
        {
            leftHand = other.gameObject;
        }
        if (other.gameObject.tag == "grabPointR")
        {
            rightHand = other.gameObject;
        }
    }

    //detects when a player's hand leaves the object's range
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "grabPointL")
        {
            if (leftHand != null)
            {
                onRelease(leftHand);
            }
            leftHand = null;
        }
        if (other.gameObject.tag == "grabPointR")
        {
            if (rightHand != null)
            {
                onRelease(rightHand);
            }
            rightHand = null;
        }
    }
}