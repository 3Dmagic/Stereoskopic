using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ToggleStereoSky))]
public class ToggleStereoSkyEditor : Editor
{
    
    private ToggleStereoSky toggleStereoSkyTarget;
    private float padding = 5;
    
    Color defaultColor;
    GUIStyle buttonStyle;

    public void OnEnable()
    {
        toggleStereoSkyTarget = (ToggleStereoSky)target;

    }



    public override void OnInspectorGUI()
    {
        GUILayout.Space(20);
        DrawDefaultInspector();
        GUILayout.Space(20);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("360 Photo")) { toggleStereoSkyTarget.ChangeSetting(false); }
        if (GUILayout.Button("Stereo Setup")) { toggleStereoSkyTarget.ChangeSetting(true); }
        GUILayout.EndHorizontal();

    }
}
