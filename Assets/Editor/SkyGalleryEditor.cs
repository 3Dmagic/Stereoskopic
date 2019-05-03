using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SkyGallery))]
public class SkyGalleryEditor : Editor
{

    private ReorderableList reorderableImageList = null;
    private SkyGallery skyGalleryTarget;
    private float padding = 5;
    
    Color defaultColor;
    GUIStyle buttonStyle;

    public void OnEnable()
    {
        skyGalleryTarget = (SkyGallery)target;

    }



    public override void OnInspectorGUI()
    {
        GUILayout.Space(20);
        DrawDefaultInspector();
        GUILayout.Space(20);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("prev")) { skyGalleryTarget.PreviousImage(); }
        if (GUILayout.Button("next")) { skyGalleryTarget.NextImage(); }
        GUILayout.EndHorizontal();

    }
}
