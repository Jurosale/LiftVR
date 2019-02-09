using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dialogueDisplay : MonoBehaviour {

	/*
	 * Deals with displaying canvas of text above NPCs head
	 * This is for prototype
	 * 
	 */ 

	//public GameObject textArea;
	//public Text display;

	// Use this for initialization
	void Start () {
		// Text Area where the canvas holds the text
		//textArea = GameObject.FindGameObjectWithTag("textCanvas");	
		// The text the NPC is saying
		//display = textArea.GetComponent<Text> ();					
	}	

	// Update is called once per frame
	void Update () {
		//Waving 
		if (Input.GetKeyDown (KeyCode.H)) {
			this.GetComponent<Text> ().text = "Hello!";
		}
		////////////////////////////////////////////
		// PRE NO AND THEN NO
		if (Input.GetKeyDown (KeyCode.U)) {
			this.GetComponent<Text> ().text = "Have you ever won an award before?";
		}
		// Shrug head no
		if (Input.GetKeyDown (KeyCode.J)) {
			this.GetComponent<Text> ().text = "Oh, that's too bad";
		}
		////////////////////////////////////////////
		// PRE YES AND THEN YES
		if (Input.GetKeyDown (KeyCode.I)) {
			this.GetComponent<Text> ().text = "Have you ever won an award before?";
		}
		// Nod head yes
		if (Input.GetKeyDown (KeyCode.K)) {
			this.GetComponent<Text> ().text = "I'm getting my first award right now.";
		}
		////////////////////////////////////////////
		/// PRE NO AND THEN NO
		if (Input.GetKeyDown (KeyCode.O)) {
			this.GetComponent<Text> ().text = "Have you ever won an award before?";
		}
		// Shrugging
		if (Input.GetKeyDown (KeyCode.A)) {
			this.GetComponent<Text> ().text = "Uh ... I guess that's alright too";
		}
		////////////////////////////////////////////
		/// When getting too close to user
		if (Input.GetKeyDown (KeyCode.O)) {
			this.GetComponent<Text> ().text = "Do you mind backing up a little?";
		}
		////////////////////////////////////////////
		// Clear text input
		if (Input.GetKeyDown (KeyCode.L)) {
			this.GetComponent<Text> ().text = "";
		}



	}
}
