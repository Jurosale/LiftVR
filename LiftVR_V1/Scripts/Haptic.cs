using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic {

	public static void rumbleController(float duration, float strength, string whichHand) {
        GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<grabHaptic>().RumbleController(duration, strength, whichHand);
    }
}
