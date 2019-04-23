using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronSpawner : MonoBehaviour
{

    public bool readyToSpawn;
    public string currentPatron;
    public int currentInteraction;
    private FloorManager fM;
    public state.floor[] targetFloors = new state.floor[5];
    //public state.floor targetFloor; //<-

    public string[] patronList = new string[5];

    void Start()
    {
        fM = GameObject.FindGameObjectWithTag("HotelManager").GetComponent<FloorManager>();
    }

    void Update()
    {
        if (readyToSpawn)
            spawnPatron();

    }


    public void spawnPatron()
    {
        if (readyToSpawn)
        {
            currentPatron = patronList[currentInteraction];
            GameObject go = (GameObject)Instantiate(Resources.Load("Patrons/" + currentPatron));
            go.transform.parent = fM.getReference(targetFloors[currentInteraction]).transform;
            Debug.Log("1 : " + go);
            readyToSpawn = false;
            return;
        }
    }

    public void updateSpawner(bool var)
    {
        readyToSpawn = var;
        currentInteraction++;
    }

}
