using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class SDKAdjust : MonoBehaviour {

    public GameObject _camera;

    VRTK_SDKManager sdkMan;
    [SerializeField]
    VRTK_SDKSetup[] sets;

    [SerializeField]
    public GameObject lControl, rControl;

    // Where camera is used
    GestureManager gm;
    GazeBehaviour gb;

	// Use this for initialization
	void Start () {
        sdkMan = GetComponent<VRTK_SDKManager>();
        gm = GetComponent<GestureManager>();
        gb = GetComponent<GazeBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        sets = sdkMan.setups;
        // find out which camera is active
        if (_camera == null)
        {
            switch (sdkMan.loadedSetup.name)
            {
                case "OculusVR":
                    _camera = sdkMan.loadedSetup.actualHeadset;
                    lControl = sdkMan.loadedSetup.actualLeftController;
                    rControl = sdkMan.loadedSetup.actualRightController;
                    break;
                case "PSVR":
                    _camera = sdkMan.loadedSetup.actualHeadset;
                    lControl = sdkMan.loadedSetup.actualLeftController;
                    rControl = sdkMan.loadedSetup.actualRightController;
                    break;
                case "SteamVR":
                    _camera = sdkMan.loadedSetup.actualHeadset;
                    lControl = sdkMan.loadedSetup.actualLeftController;
                    rControl = sdkMan.loadedSetup.actualRightController;
                    break;
                case "Simulator":
                    _camera = sdkMan.loadedSetup.actualHeadset;
                    lControl = sdkMan.loadedSetup.actualLeftController;
                    rControl = sdkMan.loadedSetup.actualRightController;
                    break;
                default:
                    break;
            }
        }
        SetUpScripts();
    }

    void SetUpScripts()
    {
        gm.CameraToMeasure = _camera.GetComponent<Camera>();
        gb.cameraEye = _camera;
    }
}
