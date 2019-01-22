using UnityEngine;
using System.Collections;
using Edwon.VR.Input;
using Edwon.VR.Gesture;

namespace Edwon.VR.Gesture
{
    public class GestureList : MonoBehaviour
    {

        VRGestureRig rig;
        IInput input;

        Transform playerHead;
        Transform playerHandL;
        Transform playerHandR;

        private string lastGesture;

        void Start() {

        }

		void Update() {
			if (GetComponent<VRGestureRig> ()) { 
				if (rig == null) {
					rig = FindObjectOfType<VRGestureRig> ();
				}
				if (playerHead == null) {
					playerHead = GameObject.FindGameObjectWithTag ("MainCamera").transform;
				}
				if (playerHandL == null) {
					playerHandL = GameObject.FindGameObjectWithTag ("leftControl").transform;
				}
				if (playerHandR == null) {
					playerHandR = GameObject.FindGameObjectWithTag ("rightControl").transform;
				}
				if (input == null) {
					input = rig.GetInput(Handedness.Right);
				}
			}
		}


        void OnEnable()
        {
            Debug.Log("now working");
            GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
        }

        void OnDisable()
        {
            Debug.Log("not working");
            GestureRecognizer.GestureDetectedEvent -= OnGestureDetected;
        }

        public void resetGesture()
        {
            lastGesture = null;
        }

        public string getGesture()
        {
            return lastGesture;
        }

        void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
        {
            string confidenceString = confidence.ToString().Substring(0, 4);
            Debug.Log("detected gesture: " + gestureName + " with confidence: " + confidenceString);
         
            lastGesture = gestureName;
           

            //Debug.Log(lastGesture);

            switch (lastGesture)
            {
                case "Yes":
                    Debug.Log("Yes Works!");
                    break;
                case "No":
                    Debug.Log("No Works!");
                    break;
                case "Pat":
                    Debug.Log("Pat Works!");
                    break;
                case "Rude":
                    Debug.Log("Rude Works!");
                    break;
                case "Agree":
                    Debug.Log("agree Works!");
                    break;
                case "Disagree":
                    Debug.Log("disagree Works!");
                    break;
                case "Salute":
                    Debug.Log("salute Works!");
                    break;
                case "Maybe":
                    Debug.Log("maybe Works!");
                    break;
                case "Confused":
                    Debug.Log("confused Works!");
                    break;
                case "Continue":
                    Debug.Log("continue Works!");
                    break;
            }
        }
    }
}