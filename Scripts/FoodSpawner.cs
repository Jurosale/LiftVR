using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FoodSpawner : MonoBehaviour
{

    //makes values easier to test for specific conditions
    [SerializeField]
    private int numFoodSpawns;
    [SerializeField]
    private float secPerSpawn;
    [SerializeField]
    private state.foodType whichFood;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Transform prefabMeat, prefabSoup, prefabDessert;

    private int currFoodCount;
    private bool isRandom;

    // Use this for initialization
    void Start()
    {
        currFoodCount = 0;
        if (whichFood == state.foodType.Random) { isRandom = true; }
        else { isRandom = false; }
        StartCoroutine("SpawnFood");
    }

    //spawns food x times after every y seconds; x and y can be adjusted in the inspector
    IEnumerator SpawnFood()
    {
        while (currFoodCount < numFoodSpawns)
        {
            //if food is random, will randomly spawn one of the other foods
            if (isRandom)
            {
                int ran = Mathf.RoundToInt(Random.value * 100) % 3;

                if (ran == 0) { whichFood = state.foodType.Meat; }
                else if (ran == 1) { whichFood = state.foodType.Soup; }
                else { whichFood = state.foodType.Dessert; }
            }

            //otherwise it spawns the food type requested
            switch (whichFood)
            {
                case state.foodType.Meat:
                    var clone1 = Instantiate(prefabMeat, position, Quaternion.identity);
                    clone1.transform.parent = transform;
                    break;
                case state.foodType.Soup:
                    var clone2 = Instantiate(prefabSoup, position, Quaternion.identity);
                    clone2.transform.parent = transform;
                    break;
                case state.foodType.Dessert:
                    var clone3 = Instantiate(prefabDessert, position, Quaternion.identity);
                    clone3.transform.parent = transform;
                    break;
            }

            if (isRandom) { whichFood = state.foodType.Random; }

            currFoodCount++;

            yield return new WaitForSeconds(secPerSpawn);
        }
    }
}