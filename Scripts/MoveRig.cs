using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRig : MonoBehaviour {

    /// <summary>
    /// Attached to camera rig (steam for now)
    /// 
    /// Used to navigate the scene with ease
    /// Uses arrow keys
    /// </summary>

    GameObject camrig;

	// Use this for initialization
	void Start () {
        camrig = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        // up goes forward
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            camrig.transform.Translate(Vector3.forward);
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            camrig.transform.Translate(Vector3.back);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            camrig.transform.Translate(Vector3.right);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            camrig.transform.Translate(Vector3.left);

        }

    }
}
