using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRude : MonoBehaviour
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
        wait = 5f; // IMPORTANT: tweek this value if you notice the animation
                   //comes out to early or too late. Ideally, this wait time should 
                   //be the timestamp in the dialogue where you wat the animation to appear
        gesture = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gesture == 2)
        {
            if (wait <= 0f)
            {
                y += 1f / 30f;
                z += 1f / 30f;
                tutorial.transform.position = new Vector3(.3f * Mathf.Sin(Mathf.PI * z), 2f - y, -.5f + y);
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
                wait = 17.5f; // IMPORTANT: tweek this value if you notice the animation
                //is desyncing overtime with the dialogue. Ideally, this wait time should 
                //be length of dialogue (10.5? seconds) + time to respond (7 seconds)
            }
            wait -= Time.deltaTime;
        }
    }
}
