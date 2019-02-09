using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class menuExtraPath : MonoBehaviour
{
    //Add a menu item
    [MenuItem("LiftTools/Create Extra Scenario")]
    static void CreateExtraPath(MenuCommand menuCommand)
    {
        GameObject w = new GameObject("Extra Scenario");

        w.AddComponent<ExtraScenario>();

        //Assign parent to currently active floor
        GameObjectUtility.SetParentAndAlign(w, GameObject.FindGameObjectWithTag("ExtraHolder"));
        //Set the Floor to the floor currently being viewed in editor
        w.GetComponent<ExtraScenario>().floorLocation = ElevatorGlobals.currentFloor;

        //Look at the selected Objects
        foreach (var transform in Selection.transforms)
        {
            //If every selected object is an extra, make them children of the new Scenario
            if (transform.gameObject.tag == "Extra")
            {
                transform.parent = w.transform;
            }
        }

        //Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(w, "Create " + w.name);
        Selection.activeObject = w;
    }

    //Add a menu item
    [MenuItem("LiftTools/Make Extra Mobile")]
    static void giveExtraMovement(MenuCommand menuCommand)
    {
        //Look at the selected Objects
        foreach (var transform in Selection.transforms)
        {
            //If the selected item is an extra, give them a path with one waypoint extended
            if (transform.gameObject.tag == "Extra")
            {
                transform.gameObject.AddComponent<extraPath>();
                transform.gameObject.GetComponent<extraPath>().AddWaypoint();
            }
        }
    }
}
