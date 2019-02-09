using UnityEngine;
using System.Collections;
using NewtonVR;

namespace NewtonVR.Example
{
	public class buttonDoorClose : MonoBehaviour
	{
		//public NVRButton Button;
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

		}

		private void Update() {
			//float distCovered = (Time.time - startTime) * speed;
			//float fracJourney = distCovered / journeyLength;
			if (down) {
				// When button is pressed
				// Open door based on button pressed
				Debug.Log("PRESSED!!!");
				doorIsClosing = true;
			}

			if (doorIsClosing) {
				openDoor.transform.position = Vector3.Lerp (openDoor.transform.position, closedDoor.transform.position,Time.deltaTime);
				doorClosedbool = true;
			}
		}
	}
}