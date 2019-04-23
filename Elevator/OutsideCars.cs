using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car
{
    public GameObject frame;
    public Vector3 destination;
    public float speed;

    public Car(GameObject f, Vector3 d, float s)
    {
        frame = f;
        destination = d;
        speed = s;
    }

}

public class OutsideCars : MonoBehaviour {

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;

    public GameObject[] carPool;
    private List<Car> activeCars;

    public float carSpeed;

    //Between 1 and 100, used a chance to spawn the next car
    [Range(1, 100)]
    public int spawnFrequency;
    private int internalTimer = 100;

    // Use this for initialization
    void Start () {
        activeCars = new List<Car>();
    }
	
	// Update is called once per frame
	void Update () {

        internalTimer--;
        if(internalTimer <= spawnFrequency)
        {
            //Create new car
            if (Random.value < 0.5f)
            {
                //Spawn at point 1
                var destination = new Vector3(spawnPoint2.transform.position.x, spawnPoint1.transform.position.y, spawnPoint1.transform.position.z);
                var a = Instantiate(carPool[Random.Range(0, carPool.Length)], spawnPoint1.transform.position, Quaternion.identity, transform);
                //Reverse the rotation for this spawn point
                a.transform.rotation = Quaternion.Euler(a.transform.rotation.x, a.transform.rotation.y + 180, a.transform.rotation.z);
                new Vector3(a.transform.rotation.x, a.transform.rotation.y+180, a.transform.rotation.z);
                activeCars.Add(new Car(a, destination, carSpeed));
            }
            else
            {
                //Spawn at point 2
                var destination = new Vector3(spawnPoint1.transform.position.x, spawnPoint2.transform.position.y, spawnPoint2.transform.position.z);
                var b = Instantiate(carPool[Random.Range(0, carPool.Length)], spawnPoint2.transform.position, Quaternion.identity, transform);
                activeCars.Add(new Car(b, destination, carSpeed));
            }

            internalTimer = 100;
        }
        
        //Move the currently active cars
		for(var i = 0; i < activeCars.Count; i++)
        {
            var activeCarPosition = activeCars[i].frame.transform.position;
            if (activeCarPosition != activeCars[i].destination)
            {
                float step = carSpeed * Time.deltaTime;
                activeCars[i].frame.transform.position = Vector3.MoveTowards(activeCarPosition, activeCars[i].destination, step);
            }
            else
            {
                Destroy(activeCars[i].frame);
                activeCars.RemoveAt(i);
            }
        }
        
    }
}
