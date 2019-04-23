using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class patronWaypoint : MonoBehaviour {

    public GameObject occupyingPatron;

    //Is this the final waypoint of the sequence
    [HideInInspector]
    public bool endNode = false;
    public bool elevatorNode = false;
    public GameObject nextNode;

    //Gizmos
    private Color color;

    private void Start()
    {
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public GameObject GetNextNode()
    {
        if (!elevatorNode)
        {
            return nextNode;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, new Vector3(0.25f, 0.25f, 0.25f));
        //If there is a next node, draw an Arrow towards it
        if(nextNode)
        {
            nextNode.GetComponent<patronWaypoint>().color = color;

            var arrowShaftLength = 0.75f; //Percent of distance between object A and B that should be covered
            var arrowHeadAngle = 20.0f;
            var arrowHeadLength = 0.25f;

            Vector3 pos = transform.position;
            Vector3 direction = (nextNode.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(nextNode.transform.position, transform.position);
    
            Gizmos.DrawRay(pos, direction * (distance * arrowShaftLength));

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 top = Quaternion.LookRotation(direction) * Quaternion.Euler(180 + arrowHeadAngle, 0, 0) * new Vector3(0, 0, 1);
            Vector3 bottom = Quaternion.LookRotation(direction) * Quaternion.Euler(180 - arrowHeadAngle, 0, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction * (distance * arrowShaftLength), right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction * (distance * arrowShaftLength), left * arrowHeadLength);
            Gizmos.DrawRay(pos + direction * (distance * arrowShaftLength), top * arrowHeadLength);
            Gizmos.DrawRay(pos + direction * (distance * arrowShaftLength), bottom * arrowHeadLength);

            //Gizmos.DrawLine(transform.position, nextNode.transform.position);
        }
    }

    //For the Editor
    //Adds a Waypoint to the existing chain. Can be called on any waypoint in the chain, and will always add to currently final node
    public void appendWaypoint()
    {
        GameObject newWaypoint = new GameObject("Patron Waypoint");

        newWaypoint.AddComponent<patronWaypoint>();

        var targetParent = this.gameObject;

        //Assign the parent of this new object to be the current, if it has a parent.
        if(transform.childCount == 0)
        {
            targetParent = transform.parent.gameObject;
        }

        GameObjectUtility.SetParentAndAlign(newWaypoint, targetParent);

        var newWaypointParent = newWaypoint.transform.parent;
        var previousWaypoint = newWaypoint.transform.parent;

        //Assign this as the last waypoint in the chain
        if (newWaypointParent.GetComponent<patronWaypoint>().nextNode == null)
        {
            newWaypointParent.GetComponent<patronWaypoint>().nextNode = this.gameObject;
        }
        else
        {
            foreach(Transform child in newWaypointParent.transform)
            {
                if(child.GetComponent<patronWaypoint>().nextNode == null && child.gameObject != newWaypoint)
                {
                    child.GetComponent<patronWaypoint>().nextNode = newWaypoint;
                    previousWaypoint = child;
                    break;
                }
            }
        }

        newWaypoint.transform.localPosition = new Vector3(previousWaypoint.transform.localPosition.x +1, previousWaypoint.transform.localPosition.y, previousWaypoint.transform.localPosition.z);

        Undo.RegisterCreatedObjectUndo(newWaypoint, "Create " + newWaypoint.name);

        Selection.activeObject = newWaypoint;
    }
}
