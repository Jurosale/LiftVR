using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLines2 : MonoBehaviour
{

    public GameObject tutorial;
    private float y;
    private float z;
    private float x;
    private float curveZ;
    private float wait;
    private int gesture;

    // Use this for initialization
    void Start()
    {
        y = 0f;
        x = 0f;
        z = 0f;
        wait = 0f;
        gesture = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gesture == 1)
        {
            if (wait <= 0f)
            {
                x += 1f / 30f;
                y += 1f / 60f;
                //Debug.Log(wait);
                tutorial.transform.position = new Vector3(x, 1.5f + .5f*y, -y - .2f);
                //Debug.Log(tutorial.transform.localPosition);
            }

            if (x >= .6f)
            {
                y = 0f;
                x = 0f;
                wait = 5f;
                gesture = 2;
            }
            wait -= Time.deltaTime;
        }

        if (gesture == 2)
        {
            if (wait <= 0f)
            {
                y += 1f / 30f;
                z += 1f / 30f;
                tutorial.transform.position = new Vector3(.3f*Mathf.Sin(Mathf.PI * z), 2f - y, -.5f + y);
            }

            //if (y >= 1f)
            //{
            //z2 += 1f / 60f;
            //curveZ = -Mathf.Cos(Mathf.PI * z);
            //tutorial.transform.position = new Vector3(0, 2.5f, 1f+ curveZ + z2);
            //}

            if (y >= 1f)
            {
                y = 0f;
                z = 0f;
                x = 0f; 
                wait = 5f;
                gesture = 1;
            }
            wait -= Time.deltaTime;
        }
    }
}
