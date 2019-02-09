using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsPanel : MonoBehaviour
{

    public GameObject[] floorLights;

    //Add one patron to the specified floor
    public void lightOn(int floor)
    {
        floorLights[floor].SetActive(true);
    }

    //Add one patron to the specified floor
    public void lightOff(int floor)
    {
        floorLights[floor].SetActive(false);
    }
}
