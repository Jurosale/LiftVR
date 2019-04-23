using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(extraPath))]
public class AddExtraPath : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        extraPath myScript = (extraPath)target;
        if (GUILayout.Button("Add Waypoint"))
        {
            myScript.AddWaypoint();
        }
    }
}
