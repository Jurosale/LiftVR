using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExtraScenario : MonoBehaviour {

    [ReadOnly]public Vector3 scenarioLocation;
    //The Floor Location is currently assigned by changing the Floor in the Elevator
    [ReadOnly]public state.floor floorLocation;
    public state.group floorGrouping;

    [Header("Parameters")]
    public bool Morning = true;
    public bool Middday = true;
    public bool Night = true;
    public bool Day1 = true;
    public bool Day2 = true;
    public bool Day3 = true;

    [Header("Overhear")]
    public AudioClip overhearAudio;
    [Tooltip("The staring time it takes for the scenario to trigger. A lower time means the scenario is easier to trigger")]
    public float focusTime;
    private bool audioTriggered = false;

    //Gizmos
    private Color color;

    // Use this for initialization
    void Start () {
		color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!Application.isPlaying)
        {
            if(floorLocation != ElevatorGlobals.currentFloor)
            {
                floorLocation = ElevatorGlobals.currentFloor;
            }
            scenarioLocation = transform.position;
        }
    }

    public void EditMe()
    {
        Instantiate(this, scenarioLocation, Quaternion.identity, GameObject.FindGameObjectWithTag("ExtraHolder").transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, new Vector3(0.25f, 0.25f, 0.25f));
        foreach (Transform child in transform)
        {
            Gizmos.DrawLine(transform.position, child.position);
        }
    }
}
