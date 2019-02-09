using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(patronWaypoint))]
public class waypointEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        patronWaypoint myScript = (patronWaypoint)target;
        if (GUILayout.Button("Add Waypoint"))
        {
            myScript.appendWaypoint();
        }
    }
}
