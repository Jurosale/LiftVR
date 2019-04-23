using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
#endif

public class VRInput {

    private static Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    //The '10' and '11' index are assigned in SteamVR_TrackedObject.cs
    public static SteamVR_Controller.Device leftController { get { return SteamVR_Controller.Input((int)trackedObjL.index); } }
    public static SteamVR_Controller.Device rightController { get { return SteamVR_Controller.Input((int)trackedObjR.index); } }
    public static SteamVR_TrackedObject trackedObjL;
    public static SteamVR_TrackedObject trackedObjR;

    private static bool priorLeft;
    private static bool priorRight;

    public static void Update()
    {
        priorRight = getRightTrigger();
        priorLeft = getLeftTrigger();
    }

    public static bool getLeftTriggerDown()
    {
#if UNITY_PS4
        if(PS4Input.MoveGetAnalogButton(0, 1) > 0 && priorLeft == false)
        {
            return true;
        }
        else
        {
            return false;
        } 
#else
        if (leftController.GetPress(triggerButton) && priorLeft == false)
        {
           // Debug.Log("trig press L");

            return true;
        }
        else
        {
            return false;
        }
#endif
    }

    public static bool getRightTriggerDown()
    {
#if UNITY_PS4
        if(PS4Input.MoveGetAnalogButton(0, 0) > 0 && priorRight == false)
        {
            return true;
        }
        else
        {
            return false;
        } 
#else
        if (rightController.GetPress(triggerButton) && priorRight == false)
        {
            //Debug.Log("trig press R");

            return true;
        }
        else
        {
            return false;
        }
#endif
    }

    public static bool getLeftTrigger()
    {
#if UNITY_PS4
        if(PS4Input.MoveGetAnalogButton(0, 1) > 0)
        {
            return true;
        }
        else
        {
            return false;
        } 
#else
        if (leftController.GetPress(triggerButton))
        {
            //Debug.Log("trig press L2");

            return true;
        }
        else
        {
            return false;
        }
#endif
    }

    public static bool getRightTrigger()
    {
#if UNITY_PS4
        if(PS4Input.MoveGetAnalogButton(0, 0) > 0)
        {
            return true;
        }
        else
        {
            return false;
        } 
#else
        if (rightController.GetPress(triggerButton))
        {
          //  Debug.Log("trig press R2");

            return true;
        }
        else
        {
            return false;
        }
#endif
    }

    public static bool getLeftTriggerRelease()
    {
#if UNITY_PS4
        if(PS4Input.MoveGetAnalogButton(0, 1) > 0 && priorLeft == true)
        {
            return true;
        }
        else
        {
            return false;
        } 
#else
        if (leftController.GetPress(triggerButton) && priorLeft == true)
        {
            return true;
        }
        else
        {
            return false;
        }
#endif
    }

    public static bool getRightTriggerRelease()
    {
#if UNITY_PS4
        if(PS4Input.MoveGetAnalogButton(0, 0) > 0 && priorRight == true)
        {
            return true;
        }
        else
        {
            return false;
        } 
#else
        if (rightController.GetPress(triggerButton) && priorRight == true)
        {
            return true;
        }
        else
        {
            return false;
        }
#endif
    }
}
