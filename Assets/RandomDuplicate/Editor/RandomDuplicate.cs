using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

/*
* To create a hotkey you can use the following special characters: % (ctrl on Windows, cmd on OS X), 
* # (shift), & (alt). If no special modifier key combinations are required the key can be given after 
* an underscore. For example to create a menu with hotkey shift-alt-g use "MyMenu/Do Something #&g". 
* To create a menu with hotkey g and no key modifiers pressed use "MyMenu/Do Something _g".
*/
public class RandomDuplicate : EditorWindow {

    static Vector3 scaleMin = new Vector3(1, 1, 1);
    static Vector3 scaleMax = new Vector3(2, 2, 2);
    
    static bool scaleAxis = true;
    static bool rotateAxis = true;

    static bool[] scale = new bool[3] { true, true, true };
    static bool[] rotation = new bool[3] { false, true, false };

    [MenuItem("Window/Random Duplicate/Settings %#&D")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(RandomDuplicate));
    }

    [MenuItem("Window/Random Duplicate/Duplicate %#D")]
    public static void Duplicate()
    {
        GameObject selectedGO = Selection.activeGameObject;
        GameObject duplicated = null;
        if(!selectedGO)
        {
            Debug.LogError("Select a GameObject first");
            return;
        }

        duplicated = Instantiate(selectedGO, selectedGO.transform.position, selectedGO.transform.rotation) as GameObject;
        duplicated.name = selectedGO.name + "(rc)";

        ApplyTransformations(duplicated);

        Undo.RegisterCreatedObjectUndo(duplicated, "Random duplicated GameObject");

        if (Selection.activeGameObject != duplicated)
        {
            Selection.activeObject = duplicated;
        }

        
    }

    private static void ApplyTransformations(GameObject duplicated)
    {
        if (scaleAxis)
        {
            if (scale[0])
            {
                float randomX = UnityEngine.Random.Range(scaleMin.x, scaleMax.x);
                duplicated.transform.localScale = new Vector3(randomX, duplicated.transform.localScale.y, duplicated.transform.localScale.z);
            }

            if (scale[1])
            {
                float randomY = UnityEngine.Random.Range(scaleMin.y, scaleMax.y);
                duplicated.transform.localScale = new Vector3(duplicated.transform.localScale.x, randomY, duplicated.transform.localScale.z);
            }

            if (scale[2])
            {
                float randomZ = UnityEngine.Random.Range(scaleMin.z, scaleMax.z);
                duplicated.transform.localScale = new Vector3(duplicated.transform.localScale.x, duplicated.transform.localScale.y, randomZ);
            }
        }

        if (rotateAxis)
        {
            if (rotation[0])
            {
                Vector3 euler = new Vector3(UnityEngine.Random.Range(0.0f, 360f), duplicated.transform.eulerAngles.y, duplicated.transform.eulerAngles.z);
                duplicated.transform.eulerAngles = euler;
            }

            if (rotation[1])
            {
                Vector3 euler = new Vector3(duplicated.transform.eulerAngles.x, UnityEngine.Random.Range(0.0f, 360f), duplicated.transform.eulerAngles.z);
                duplicated.transform.eulerAngles = euler;
            }
            
            if (rotation[2])
            {
                Vector3 euler = new Vector3(duplicated.transform.eulerAngles.x, duplicated.transform.eulerAngles.y, UnityEngine.Random.Range(0.0f, 360f));
                duplicated.transform.eulerAngles = euler;
            }
        }

    }

    void OnGUI()
    {
        GUILayout.Label("Random Duplicate Settings", EditorStyles.boldLabel);

        scaleAxis = EditorGUILayout.BeginToggleGroup("Scale axis", scaleAxis);
        scale[0] = EditorGUILayout.Toggle("X", scale[0]);
        scale[1] = EditorGUILayout.Toggle("Y", scale[1]);
        scale[2] = EditorGUILayout.Toggle("Z", scale[2]);
        EditorGUILayout.EndToggleGroup();

        rotateAxis = EditorGUILayout.BeginToggleGroup("Rotate axis", rotateAxis);
        rotation[0] = EditorGUILayout.Toggle("X", rotation[0]);
        rotation[1] = EditorGUILayout.Toggle("Y", rotation[1]);
        rotation[2] = EditorGUILayout.Toggle("Z", rotation[2]);
        EditorGUILayout.EndToggleGroup();

        scaleMin = EditorGUILayout.Vector3Field("Minimum scale values", scaleMin);
        scaleMax = EditorGUILayout.Vector3Field("Maximum scale values", scaleMax);
    }

    void OnValidate()
    {
        
    }
}
