using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {

    public bool Walkable;
    public Vector3 WorldPosition { get; private set; }
    public int GridX { get; private set; }
    public int GridY { get; private set; }
    public int HeapIndex { get; set; }

    public int GCost;
    public int HCost;
    public Node Parent;

    public int FCost {
        get
        {
            return GCost + HCost;
        }
    }

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        Walkable = walkable;
        WorldPosition = worldPos;
        GridX = gridX;
        GridY = gridY;
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);

        if (compare == 0)
            compare = HCost.CompareTo(nodeToCompare.HCost);

        return -compare;
    }

}
