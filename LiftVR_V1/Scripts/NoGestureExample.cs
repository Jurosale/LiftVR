using UnityEngine;
using System.Collections;
using Edwon.VR.Input;

namespace Edwon.VR.Gesture
{
    public class NoGestureExample : MonoBehaviour
    {
        public GameObject fire;
        public GameObject earth;
        public GameObject ice;
        public GameObject air;

        VRGestureRig rig;
        IInput input;

        Transform playerHead;
        Transform playerHandL;
        Transform playerHandR;

        void Start()
        {
            rig = FindObjectOfType<VRGestureRig>();
            if (rig == null)
            {
                Debug.Log("there is no VRGestureRig in the scene, please add one");
            }

            playerHead = rig.head;
            playerHandR = rig.handRight;
            playerHandL = rig.handLeft;

            input = rig.GetInput(rig.mainHand);
        }

        void Update()
        {

        }

        void OnEnable()
        {
            Debug.Log("now working");
            GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
        }

        void OnDisable()
        {
            Debug.Log("now not working");
            GestureRecognizer.GestureDetectedEvent -= OnGestureDetected;
        }

        void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
        {
            string confidenceString = confidence.ToString().Substring(0, 4);
            Debug.Log("detected gesture: " + gestureName + " with confidence: " + confidenceString);

            switch (gestureName)
            {
                case "TestNo":
                    Debug.Log("TestNo Works!");
                    break;
                case "TestYes":
                    Debug.Log("TestYes Works!");
                    break;
                case "RWave1":
                    Debug.Log("RWave1 Works!");
                    break;
                case "LWave1":
                    Debug.Log("LWave1 Works!");
                    break;
                case "Line":
                    Debug.Log("Line Works!");
                    break;
                case "Pull":
                    //DoPull();
                    break;
            }
        }
    }
}