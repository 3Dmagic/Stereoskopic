using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(StereoImageGallery))]
public class ImageGalleryEditor : Editor
{

    private ReorderableList reorderableImageList = null;
    private StereoImageGallery m_ImageDatabase;
    private float padding = 5;
    
    Color defaultColor;
    GUIStyle buttonStyle;

    public void OnEnable()
    {
        m_ImageDatabase = (StereoImageGallery)target;
        InitReorderable();
    }

    private void InitReorderable()
    {
        reorderableImageList = new ReorderableList( m_ImageDatabase.images,typeof(StereoImages), true, true, true, true);
        reorderableImageList.drawElementCallback = DrawSingleImage;
        reorderableImageList.drawHeaderCallback += DrawHeader;
        reorderableImageList.elementHeight = 125;
    }

    public void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "StereoImages", EditorStyles.boldLabel);
    }


    public void DrawSingleImage(Rect rect, int index, bool isActive, bool isFocused){

        Texture2D left = m_ImageDatabase.images[index].leftEyeImage;
        Texture2D right = m_ImageDatabase.images[index].rightEyeImage;
        Texture2D sky = m_ImageDatabase.images[index].skyImage;
        AudioClip audio = m_ImageDatabase.images[index].skyAudio;
        string description = m_ImageDatabase.images[index].description;

        GUI.Label(new Rect(rect.x + 5,rect.y + 25, 50, EditorGUIUtility.singleLineHeight)  , index.ToString() );

        //Left Image
        GUI.Label(new Rect(rect.x + 50 ,rect.y, 50, EditorGUIUtility.singleLineHeight)  , "Left");
        m_ImageDatabase.images[index].leftEyeImage = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x + 50, rect.y + EditorGUIUtility.singleLineHeight + padding, 50, 50), "", left, typeof(Texture2D), true);
        //Right Image
        GUI.Label(new Rect(rect.x + 100 + padding, rect.y,50, EditorGUIUtility.singleLineHeight), "Right");
        m_ImageDatabase.images[index].rightEyeImage = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x + 100 + padding, rect.y + EditorGUIUtility.singleLineHeight + padding, 50, 50), "", right, typeof(Texture2D), true);

        //SKY
        GUI.Label(new Rect(rect.x + 170 + padding, rect.y,50, EditorGUIUtility.singleLineHeight), "SKY");
        m_ImageDatabase.images[index].skyImage = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x + padding+170, rect.y + EditorGUIUtility.singleLineHeight + padding, 50,50), "", sky, typeof(Texture2D), true);
        //SKY Audio
        GUI.Label(new Rect(rect.x + 230 + padding, rect.y + EditorGUIUtility.singleLineHeight + padding, 50, EditorGUIUtility.singleLineHeight), "Audio");
        m_ImageDatabase.images[index].skyAudio = (AudioClip)EditorGUI.ObjectField(new Rect(rect.x + 265 + padding, rect.y + EditorGUIUtility.singleLineHeight + padding, 250, EditorGUIUtility.singleLineHeight), "", audio, typeof(AudioClip), true);


        //Description
        GUI.Label(new Rect(rect.x + 230 + padding, rect.y + (EditorGUIUtility.singleLineHeight * 2) + (padding), 70, EditorGUIUtility.singleLineHeight), "Description");
        m_ImageDatabase.images[index].description = EditorGUI.TextArea(new Rect(rect.x + 230 + padding, rect.y + (EditorGUIUtility.singleLineHeight * 3) + (padding), 300, 50), description);
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(20);
        DrawDefaultInspector();
        GUILayout.Space(20);

        serializedObject.Update();       
            reorderableImageList.DoLayoutList();       
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(m_ImageDatabase);


        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("prev")) { m_ImageDatabase.PreviousImage(); }
        if (GUILayout.Button("next")) { m_ImageDatabase.NextImage(); }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("360°")) { m_ImageDatabase.SetStereoSky(false, true); m_ImageDatabase.ShowSkyImage(true); }
        if (GUILayout.Button("Stereo")) { m_ImageDatabase.SetStereoSky(false, false); m_ImageDatabase.ShowSkyImage(false); }
        GUILayout.EndHorizontal();

    }
}
