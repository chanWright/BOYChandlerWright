using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;

public class NodeEditorWindow : EditorWindow {

	public Stitch thisNode;
    public int id;
    ScriptableObject myStitch;
    Vector2 scrollPos;
	public static NodeEditorWindow ShowWindow()
    {
        return GetWindow<NodeEditorWindow>();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginScrollView(scrollPos);
		thisNode.stitchName = EditorGUILayout.TextField("Stitch Name:", thisNode.stitchName);
		thisNode.summary = EditorGUILayout.TextField("Summary:", thisNode.summary);

		thisNode.background = (Sprite)EditorGUILayout.ObjectField(thisNode.background, typeof(Sprite), false);
		if(thisNode.background != null)
		{
			Texture2D atext = SpriteUtility.GetSpriteTexture(thisNode.background, false);
			GUILayout.Label(atext);
		}
        myStitch = thisNode;
        SerializedObject so = new SerializedObject(myStitch);
        SerializedProperty performers = so.FindProperty("performers");
        EditorGUILayout.PropertyField(performers, true);
        so.ApplyModifiedProperties();

        SerializedProperty dialogs = so.FindProperty("dialogs");
        EditorGUILayout.PropertyField(dialogs, true);
        so.ApplyModifiedProperties();

        SerializedProperty yarns = so.FindProperty("yarns");
        EditorGUILayout.PropertyField(yarns, true);
        so.ApplyModifiedProperties();

        thisNode.status = (Stitch.stitchStatus)EditorGUILayout.EnumPopup("Status", thisNode.status);
        EditorGUILayout.EndScrollView();
    }
}
