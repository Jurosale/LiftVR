using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxTransition : MonoBehaviour {

    public Material skybox;
    private float thickness;

	// Use this for initialization
	void Start () {
        skybox.SetFloat("_Exposure", 2.4f);
        thickness = 0.3f;
        //sky tint = 7E7575FF
        //ground = 313231FF 
    }

    // Update is called once per frame
    void Update () {
        if (thickness < 4.3) { thickness += Time.deltaTime / 150; }
        skybox.SetFloat("_AtmosphereThickness", thickness);
    }
}
