using UnityEngine;
using UnityEditor;
using System.Collections;

public class PathNode : MonoBehaviour {

    /// <summary>
    /// Useful for drawing Gizmos
    /// </summary>
    public PathNode PreviousNode;

    /// <summary>
    /// Controls how long or even if a character should hold at a node
    /// </summary>
    public bool HoldOnReach;
    public float HoldSeconds;
    
    /// <summary>
    /// Draws the node in the editor and a line to the previous node
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "PathNode.png");

        if(PreviousNode != null)
            Gizmos.DrawLine(transform.position, PreviousNode.transform.position);
    }
}

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{
    /// <summary>
    /// No need to show the Previous Node, only build nodes via Path component.
    /// Hide HoldSeconds if HoldOnReach is not set.
    /// </summary>
    public override void OnInspectorGUI()
    {
        PathNode myScript = (PathNode)target;

        myScript.HoldOnReach = GUILayout.Toggle(myScript.HoldOnReach, "Hold");

        if (myScript.HoldOnReach)
            myScript.HoldSeconds = EditorGUILayout.FloatField("Hold in Seconds", myScript.HoldSeconds);
    }
}