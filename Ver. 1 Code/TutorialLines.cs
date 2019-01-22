using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLines : MonoBehaviour {

    public GameObject tutorial;
    private float y;
    private float z;
    private float z2;
    private float curveZ;
    private float wait;
    private int gesture;

	// Use this for initialization
	void Start () {
        y = 0f;
        z = 0f;
        z2 = 0f;
        wait = 6.5f;
        gesture = 1;
	}
	
	// Update is called once per frame
	void Update (){
        if (gesture == 1)
        {
            if (wait <= 0f)
            {
                y += 1f / 30f;
                //Debug.Log(wait);
                tutorial.transform.position = new Vector3(0, .2f * Mathf.Sin(Mathf.PI * y) + 1.5f, .2f * Mathf.Cos(Mathf.PI * y));
                //Debug.Log(tutorial.transform.localPosition);
            }

            if (y >= 2f)
            {
                y = 0f;
                wait = 3.5f;
                gesture = 2;
            }
            wait -= Time.deltaTime;
        }

        if (gesture == 2)
        {
            if (wait <= 0f)
            {
                y += 1f / 30f;
                if (y >= .3f) {
                    z += 1f / 12f;
                    tutorial.transform.position = new Vector3(0, .25f * Mathf.Sin(Mathf.PI * z) + 1.7f, -.25f * Mathf.Cos(Mathf.PI * z));
                }
                else { tutorial.transform.position = new Vector3(0, y + 1.3f, -.25f); }
            }

            //if (y >= 1f)
            //{
                //z2 += 1f / 60f;
                //curveZ = -Mathf.Cos(Mathf.PI * z);
                //tutorial.transform.position = new Vector3(0, 2.5f, 1f+ curveZ + z2);
            //}

            if (y >= .6f)
            {
                y = 0f;
                z = 0f;
                z2 = 0f;
                wait = 16.4f;
                gesture = 1;
            }
            wait -= Time.deltaTime;
        }
    }
}
