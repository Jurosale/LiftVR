using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftableRange : MonoBehaviour {

    public doorInteraction doorManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "grabPoint")
        {
            Debug.Log(other.gameObject.name + " entered");
            doorInteraction.handInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "grabPoint")
        {
            Debug.Log(other.gameObject.name + " EXITED");
            doorInteraction.handInRange = false;
        }
    }
}
