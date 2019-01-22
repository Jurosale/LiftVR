using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSalute : MonoBehaviour
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
        wait = 6f; // IMPORTANT: tweek this value if you notice the animation
                   //comes out to early or too late. Ideally, this wait time should 
                   //be the timestamp in the dialogue where you wat the animation to appear
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
                tutorial.transform.position = new Vector3(x - .4f, 1.5f + .5f * y, -y + .3f);
                //Debug.Log(tutorial.transform.localPosition);
            }

            if (x >= .6f)
            {
                y = 0f;
                x = 0f;
                wait = 15f; // IMPORTANT: tweek this value if you notice the animation
                //is desyncing overtime with the dialogue. Ideally, this wait time should 
                //be length of dialogue (8? seconds) + time to respond (7 seconds)
            }
            wait -= Time.deltaTime;
        }
    }
}