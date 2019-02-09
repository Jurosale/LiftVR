using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This code creates the simple button which appears 

[CustomEditor(typeof(extraPath))]
public class extraPathBuilder : Editor {

    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        extraPath myScript = (extraPath)target;
        if (GUILayout.Button("Add Waypoint"))
        {
            myScript.AddWaypoint();
        }
    }
}
