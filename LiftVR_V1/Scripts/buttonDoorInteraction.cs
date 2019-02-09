using UnityEngine;
using System.Collections;
using NewtonVR;

namespace NewtonVR.Example
{
	public class buttonDoorInteraction : MonoBehaviour
	{
//		public NVRButton Button;
		bool down;

		public GameObject closedDoor;
		public GameObject openDoor;

		private float startTime;
		private float journeyLength;
		public float speed = 1.0f;
		bool doorOpenbool = false;
		bool doorClosedbool = true;
		bool doorIsOpening = false;
		bool doorIsClosing = false;
		float distCovered;
		float fracJourney;

		void Start() {
			startTime = Time.time;
			journeyLength = Vector3.Distance (closedDoor.transform.position, openDoor.transform.position);

		}

		private void Update() {
			//float distCovered = (Time.time - startTime) * speed;
			//float fracJourney = distCovered / journeyLength;
			if (down) { //Button.ButtonDown
				// When button is pressed
				// Open door based on button pressed
				Debug.Log("PRESSED!!!");
				doorIsOpening = true;
			}

			if (doorIsOpening) {
				closedDoor.transform.position = Vector3.Lerp (closedDoor.transform.position, openDoor.transform.position,Time.deltaTime);

				doorOpenbool = true;

			}
		}
	}
}