using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraPath : MonoBehaviour
{

    //The class for nodes which extras use as waypoints

    [ReadOnly] public bool startNode = true;

    //The reference to the next path node, if no next node then the path is complete
    [ReadOnly] public GameObject nextNode;

    private float speed = 1;
    private Vector3 holdLocation;

    private void Awake()
    {
        holdLocation = transform.position;
    }

    private void Update()
    {
        //If this is the startNode (the extra path attached to the Extra Model)
        if (startNode && nextNode)
        {
            if (transform.position != nextNode.transform.position)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, step);
            }
            else
            {
                //If there is another node on this path
                if (nextNode.GetComponent<extraPath>().nextNode)
                {
                    var oldNext = nextNode;
                    nextNode = nextNode.GetComponent<extraPath>().nextNode;
                    Destroy(oldNext);
                }
            }
        }
        else if (!startNode && transform.position != holdLocation)
        {
            transform.position = holdLocation;
        }
    }
    //Add waypoint will extend the path for the current extra. This will always be called 
    //from the current end node regardless of the node where the button was pressed
    public void AddWaypoint()
    {
        var o = gameObject;

        if (nextNode)
        {
            nextNode.GetComponent<extraPath>().AddWaypoint();
            return;
        }

        //Create a clone of the existing waypoint, with the same rotation and parent
        var offset = new Vector3(0, 0, 5);
        var spawnPosition = o.transform.position + offset;

        //If the first path being created, make it child of the extra, else make it same child as previous waypoint
        var parent = o.transform.parent;
        if(tag == "Extra")
        {
            parent = this.transform;
        }

        GameObject newW = new GameObject();
        newW.transform.position = spawnPosition;
        newW.transform.parent = parent;
        newW.name = "Extra Path v2";
        newW.AddComponent<extraPath>();
        
        o.GetComponent<extraPath>().nextNode = newW;
        newW.GetComponent<extraPath>().startNode = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1, 1);
        
        Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));

        if (nextNode)
        {
            Gizmos.DrawLine(transform.position, nextNode.transform.position);
        }
    }
}

