using UnityEngine;
using System.Collections;

/*
 * Attached to the NPC
 * There are 2 NPC objects
 * One goes through door and lerps to other object position
 * Set one active and then set the other off
 * 
 */ 

public class fakeAI : MonoBehaviour {

	public GameObject start;
	public GameObject dest;
	bool npcGo;
	bool arrived = false;

	// Use this for initialization
	void Start () {
		dest.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// If D is pressed
		// Will be pressed after door is opened
		if (Input.GetKeyDown (KeyCode.Space)) {
			// NPC 1 is already enabled
			npcGo = true;
		}

		if (npcGo) {
			start.transform.position = Vector3.Lerp (start.transform.position, dest.transform.position, Time.deltaTime);
			arrived = true;
		}

	}
}
