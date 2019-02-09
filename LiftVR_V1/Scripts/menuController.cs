using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {

    private GameObject menu;

    // Use this for initialization
    void Start () {

        menu = GameObject.FindGameObjectWithTag("Menu");

        if (SceneManager.GetActiveScene().name == "main") {
            menu.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
