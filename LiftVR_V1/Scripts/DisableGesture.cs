using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;
using Edwon.VR.Input;

public class DisableGesture : MonoBehaviour {

	public void turnOff() {
		if (GetComponent<VRGestureRig>()) {
			VRControllerInputSteam[] cis = GetComponents<VRControllerInputSteam> ();
			GestureTrail[] gt = GetComponents<GestureTrail> ();

			Destroy(this.GetComponent<VRGestureRig>());

			foreach (Transform t in GetComponentsInChildren<Transform> ()) {
				if (t.name == "Trail Renderer") 
					Destroy (t.gameObject);
			}
			foreach (VRControllerInputSteam s in cis) 
				Destroy (s);
			foreach (GestureTrail g in gt) 
				Destroy (g);
		}
	}

	public void turnOn() {
		if (!GetComponent<VRGestureRig> () && !LeverRange.inRange && !doorInteraction.handInRange && !Interactable.inRange) {
			this.gameObject.AddComponent<VRGestureRig>();
			this.GetComponent<VRGestureRig> ().AutoSetup ();
            this.GetComponent<VRGestureRig>().spawnControllerModels = false;
		} 
	}

	public bool isComponentEnabled() {
		if (GetComponent<VRGestureRig>()) {
				return true;
		}
		return false;
	}
}
