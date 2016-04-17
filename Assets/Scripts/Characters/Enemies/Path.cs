using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Path : MonoBehaviour {

    public List<PathNode> Points;
    public PathNode CurrentPoint { get; private set; }
    public bool DestroyOnEnd;
    public Transform ControlledEnemy;

    private int _counter = 0;
    private Stopwatch _holdTimer = new Stopwatch();

    // Use this for initialization
    void Start () {
        CurrentPoint = GetNext();
	}
	
	// Update is called once per frame
	void Update () {
        if (ControlledEnemy.position == CurrentPoint.transform.position)
        {            
            if(CurrentPoint.HoldOnReach)
            {
                if (!_holdTimer.IsRunning)
                    _holdTimer.Start();
                else if((float)_holdTimer.ElapsedMilliseconds / 1000 >= CurrentPoint.HoldSeconds)
                {
                    CurrentPoint = GetNext();
                    _holdTimer.Reset();
                    _holdTimer.Stop();
                }                 
            }
            else
            {
                CurrentPoint = GetNext();
            }
        }
    }

    private PathNode GetNext()
    {
        if (_counter >= Points.Count)
        {
            if (DestroyOnEnd)
                Destroy(transform.parent.gameObject);

            return CurrentPoint;
        }

        PathNode t = Points[_counter];
        _counter++;
        
        return t;
    }

    /// <summary>
    /// Only call in Editor to build a path
    /// </summary>
    public void BuildNewPathNode()
    {
        GameObject g = new GameObject("Node " + Points.Count, typeof(PathNode));
        g.transform.position = transform.position;
        g.transform.parent = gameObject.transform;
        PathNode p = g.GetComponent<PathNode>();

        if (Points.Count > 0)
            p.PreviousNode = Points[Points.Count - 1];

        Points.Add(p);
    }
}

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path myScript = (Path)target;
        if (GUILayout.Button("Add Node"))
        {
            myScript.BuildNewPathNode();
        }
    }
}