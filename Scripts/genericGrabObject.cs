using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A grabbable object requires a Rigidbody and collider with a trigger
[RequireComponent(typeof(Rigidbody))]
public abstract class GenericGrabObject : MonoBehaviour
{

    //Is null if hand not in range, assigned to the gameObject colliding with otherwise
    protected GameObject rightHand = null;
    protected GameObject leftHand = null;

    //gives every grabbable object different grab states and implementations
    protected abstract void OnGrab(GameObject grabber);
    protected abstract void WhileGrabbed(GameObject grabber);
    protected abstract void OnRelease(GameObject grabber);

    // Update is called once per frame
    void Update()
    {
        if (VRInput.getRightTriggerDown() && rightHand != null)
        {
            OnGrab(rightHand);
        }
        if (VRInput.getLeftTriggerDown() && leftHand != null)
        {
            OnGrab(leftHand);
        }
        if (VRInput.getRightTrigger() && rightHand != null)
        {
            WhileGrabbed(rightHand);
        }
        if (VRInput.getLeftTrigger() && leftHand != null)
        {
            WhileGrabbed(leftHand);
        }
        if (VRInput.getRightTriggerRelease() && rightHand != null)
        {
            OnRelease(rightHand);
        }
        if (VRInput.getLeftTriggerRelease() && leftHand != null)
        {
            OnRelease(leftHand);
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
                OnRelease(leftHand);
            }
            leftHand = null;
        }
        if (other.gameObject.tag == "grabPointR")
        {
            if (rightHand != null)
            {
                OnRelease(rightHand);
            }
            rightHand = null;
        }
    }
}