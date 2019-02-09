using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExtraScenario))]
public class EditScenario : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ExtraScenario myScript = (ExtraScenario)target;
        if (GUILayout.Button("Edit Scenario"))
        {
            myScript.EditMe();
        }
    }
}
