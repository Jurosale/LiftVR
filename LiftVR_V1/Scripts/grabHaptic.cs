using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabHaptic : MonoBehaviour {

    private string WhichHand;
    private float Strength;
    private bool rumbling;

    public void RumbleController(float duration, float strength, string whichHand) {
        if(strength < 0 || strength > 10) {
            Debug.LogWarning("Haptic strength is set to: " + strength + " Must be set between 0 and 10");
        }

        if (whichHand != "left" && whichHand != "right" && whichHand != "both") {
            Debug.LogError("RumbleController requires a third argument of 'left', 'right' or 'both'. Set to both instead");
            StartCoroutine(RumbleControllerRoutine(duration, strength, "both"));
        }
        else {
            StartCoroutine(RumbleControllerRoutine(duration, strength * 399, whichHand));
            //rumbling = true;
            //Strength = strength;
            //WhichHand = whichHand;
        }
    }

    IEnumerator RumbleControllerRoutine(float duration, float strength, string whichHand) {

        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime <= duration) {
            int valveStrength = Mathf.RoundToInt(Mathf.Lerp(0, 3999, strength));
            if(whichHand == "left" || whichHand == "both") {
                var deviceIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>().leftControlIndex;
                SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)strength);
            }
            if(whichHand == "right" || whichHand == "both") {
                var deviceIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>().rightControlIndex;
                SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)strength);
            }

            yield return null;
        }
    }


    public void Update() {
        if (rumbling) {
            Strength = 3000;//Mathf.RoundToInt(Mathf.Lerp(0, 3999, Strength));
            if (WhichHand == "left" || WhichHand == "both") {
                var deviceIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>().leftControlIndex;
                SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)Strength);
            }
            if (WhichHand == "right" || WhichHand == "both") {
                var deviceIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>().rightControlIndex;
                SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)Strength);
            }
        }
    }
}
