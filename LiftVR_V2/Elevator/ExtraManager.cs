using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraManager : MonoBehaviour {

    public GameObject extraHolder;

    private List<GameObject> extraScenarios;

    private GameObject toSpawn;

    // Use this for initialization
    void Start () {
        extraHolder = GameObject.FindGameObjectWithTag("ExtraHolder");

         var temp = Resources.LoadAll("Extra Scenarios", typeof(GameObject));

        //At the start of the game create lists for every floor loaded from Resources
        extraScenarios = new List<GameObject>();
        for (var i = 0; i < temp.Length; i++)
        {
            extraScenarios.Add(temp[i] as GameObject);
        }
    }

    public void clearExtras()
    {
        //foreach (Transform child in extraHolder.transform)
        //{
        //    Destroy(child.gameObject);
        //}
    }

    public void loadNewExtras(state.floor target)
    {
        //Find all scenarios of our floor for Group A, and are correct for the time of day
        var Item = extraScenarios.FindAll(c => (c.GetComponent<ExtraScenario>().floorLocation == target) && (c.GetComponent<ExtraScenario>().floorGrouping == state.group.GroupA));
        
        //If we have at least 1 scenario to spawn
        if (Item.Count > 0)
        {
            //Choose a random scenario
            toSpawn = Item[Random.Range(0, Item.Count)];

            //Spawn the Scenario for Group A
            var a = Instantiate(toSpawn, toSpawn.GetComponent<ExtraScenario>().scenarioLocation, Quaternion.identity, extraHolder.transform);
        }
        else
        {
            //Spawn Nothing Currently
        }

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
