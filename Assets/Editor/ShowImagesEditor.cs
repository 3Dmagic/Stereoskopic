using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ShowImages))]
public class ShowImagesEditor : Editor
{
    ShowImages _showImages;
    public void OnEnable()
    {
        _showImages = (ShowImages)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(20);
        DrawDefaultInspector();
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Show 360°")) { _showImages.ShowGlass(false); }
        if (GUILayout.Button("Show Stereo")) { _showImages.ShowGlass(true); }
        GUILayout.EndHorizontal();

    }
}
