using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeExample : EditorWindow {

    public List<NodeBaseClass> myNodes = new List<NodeBaseClass>();

    public Spool mySpool;

    public int nodeAttachID = -1;
    [MenuItem("Node Editor/ Editor")]
	public static void showWindow()
    {
        GetWindow<NodeExample>();
    }

    public void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        mySpool = (Spool)EditorGUILayout.ObjectField(mySpool, typeof(Spool), false);
        if (EditorGUI.EndChangeCheck())
        {
            myNodes.Clear();
            if (mySpool != null)
            {
                for(int i = 0; i < mySpool.stitchCollection.Length; i++)
                {
                    myNodes.Add(new StitchNode(new Rect(20 + (125 * i), 20, 120, 120), i, mySpool.stitchCollection[i]));   
                }
            }
        }
        if (mySpool != null)
        {
            for (int i = 0; i < mySpool.stitchCollection.Length; i++)
            {
                for (int j = 0; j < mySpool.stitchCollection[i].yarns.Length; j++)
                {
                    if (mySpool.stitchCollection[i].yarns[j].choiceStitch != null)
                    {
                        DrawNodeCurve(myNodes[i].rect, myNodes[mySpool.stitchCollection[i].yarns[j].choiceStitch.stitchID].rect);
                    }
                }
            }
        }

       BeginWindows();
        for(int i = 0; i < myNodes.Count; i++)
        {
            myNodes[i].rect = GUI.Window(i, myNodes[i].rect, myNodes[i].DrawGUI, mySpool.stitchCollection[i].stitchName);
        }
        EndWindows();

    }

    public void RemoveNode(int id)
    {
        for (int i = 0; i < myNodes.Count; i++)
        {
            myNodes[i].linkedNodes.RemoveAll(item => item.id == id);
        }
        myNodes.RemoveAt(id);
        UpdateNodeIDs();
    }

    public void UpdateNodeIDs()
    {
        for(int i = 0; i < myNodes.Count; i++)
        {
            myNodes[i].ReassignID(i);
        }
    }

    public void BeginAttachment(int winID)
    {
        nodeAttachID = winID;
    }
        
    public void EndAttachment(int winID)
    {
        if (nodeAttachID > -1)
        {
            myNodes[nodeAttachID].AttachComplete(myNodes[winID]);
        }
        nodeAttachID = -1;
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + (start.height / 2)+10, 0);
        Vector3 endPos = new Vector3(end.x, end.y + (end.height / 2)+ 10, 0);
        Vector3 startTan = startPos + Vector3.right * 100;
        Vector3 endTan = endPos + Vector3.left * 100;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

       
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.white, null, 3);
    }

}
