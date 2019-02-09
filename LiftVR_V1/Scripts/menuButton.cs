using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButton : MonoBehaviour {

    private bool handInRange = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "grabPoint") {
            handInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "grabPoint") {
            handInRange = false;
        }
    }

    public void attemptGrab() {
        if (handInRange) {
            SceneManager.LoadScene("main");
        }
    }
}
