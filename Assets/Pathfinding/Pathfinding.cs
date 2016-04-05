using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour {

    public Transform Seeker, Target;

    private Grid m_grid;
    private Stopwatch m_sw;

    void Awake()
    {
        m_grid = GetComponent<Grid>();
        m_sw = new Stopwatch();
    }

    void Start()
    {
        m_sw.Start();
    }

    void Update()
    {
        if(m_sw.ElapsedMilliseconds > 100)
        {
            FindPath(Seeker.position, Target.position);
            m_sw.Reset();
            m_sw.Start();
        }
    }

	public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Node startNode = m_grid.NodeFromWorldPoint(startPos);
        Node targetNode = m_grid.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(m_grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                sw.Stop();
                print("Path Found: " + sw.ElapsedMilliseconds + "ms");
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbor in m_grid.GetNeighbors(currentNode))
            {
                if (!neighbor.Walkable || closedSet.Contains(neighbor))
                    continue;

                int newMovementCostToNeightbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                if(newMovementCostToNeightbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeightbor;
                    neighbor.HCost = GetDistance(neighbor, targetNode);
                    neighbor.Parent = currentNode;
                    
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else
                        openSet.UpdateItem(neighbor);
                }
            }
        }
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        m_grid.Path = path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        else
            return 14 * dstX + 10 * (dstY - dstX);
    }
}
