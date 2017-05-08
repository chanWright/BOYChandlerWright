using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StitchNode : NodeBaseClass {

    Stitch myStitch;
	public NodeEditorWindow nodeWindow;

    public StitchNode(Rect r, int ID, Stitch stitchInfo) : base(r,ID)
    {
        myStitch = stitchInfo;
    }

    public override void DrawGUI(int winID)
    {
		GUIStyle myStyle = new GUIStyle();
		myStyle.wordWrap = true;
		myStyle.alignment = TextAnchor.MiddleCenter;
		EditorGUILayout.LabelField ("Summary:\n" + myStitch.summary, myStyle);
        BaseDraw();
    }

    public override void AttachComplete(NodeBaseClass winID)
    {
        base.linkedNodes.Add(winID);
    }

	public override void EditNode(){
		nodeWindow = NodeEditorWindow.ShowWindow();
		nodeWindow.thisNode = myStitch;
	}

}
