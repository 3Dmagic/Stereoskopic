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
        reorderableImageList.elementHeight = 65;
    }

    public void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "StereoImages", EditorStyles.boldLabel);
    }


    public void DrawSingleImage(Rect rect, int index, bool isActive, bool isFocused){

        Texture2D left = m_ImageDatabase.images[index].leftEyeImage;
        Texture2D right = m_ImageDatabase.images[index].rightEyeImage;

        GUI.Label(new Rect(rect.x + 5,rect.y + 25, 50, EditorGUIUtility.singleLineHeight)  , index.ToString() );

        GUI.Label(new Rect(rect.x + 50 ,rect.y + 25, 50, EditorGUIUtility.singleLineHeight)  , "Left");
        m_ImageDatabase.images[index].leftEyeImage = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x + 80, rect.y + padding, 50, 50), "", left, typeof(Texture2D), true);
        GUI.Label(new Rect(rect.x + 150, rect.y + 25,50, EditorGUIUtility.singleLineHeight), "Right");
        m_ImageDatabase.images[index].rightEyeImage = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x + 190, rect.y + padding, 50, 50), "", right, typeof(Texture2D), true);
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

    }
}
