using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Grid : MonoBehaviour {

    public bool OnlyDisplayPathGizmos;
    public Transform Player;
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize;
    public float NodeRadius;
    public int MaxSize
    {
        get
        {
            return m_gridSizeX * m_gridSizeY;
        }
    }

    public List<Node> Path;

    private Node[,] m_grid;
    private float m_nodeDiameter;
    private int m_gridSizeX, m_gridSizeY;
    private Stopwatch m_sw;


    void Start()
    {
        m_nodeDiameter = NodeRadius * 2;
        m_gridSizeX = Mathf.RoundToInt(GridWorldSize.x / m_nodeDiameter);
        m_gridSizeY = Mathf.RoundToInt(GridWorldSize.y / m_nodeDiameter);

        CreateGrid();
        m_sw = new Stopwatch();
        m_sw.Start();
    }

    void Update()
    {
        if(m_sw.ElapsedMilliseconds > 100)
        {
            UpdateGrid();
            m_sw.Reset();
            m_sw.Start();
            
        }
    }


    private void CreateGrid()
    {
        m_grid = new Node[m_gridSizeX, m_gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        for (int x = 0; x < m_gridSizeX; x++)
        {
            for(int y = 0; y < m_gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * m_nodeDiameter + NodeRadius) + Vector3.forward * (y * m_nodeDiameter + NodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask));

                m_grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public void UpdateGrid()
    {
        foreach(Node node in m_grid)
            node.Walkable = !(Physics.CheckSphere(node.WorldPosition, NodeRadius, UnwalkableMask));
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < m_gridSizeX && checkY >= 0 && checkY < m_gridSizeY)
                    neighbors.Add(m_grid[checkX, checkY]);
            }
        }

        return neighbors;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((m_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((m_gridSizeY - 1) * percentY);

        return m_grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

        if (OnlyDisplayPathGizmos)
        {
            if (Path != null) {
                foreach (Node n in Path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (m_nodeDiameter - 0.1f));
                }
            }
        }


        if(m_grid != null)
        {
            Node playerNode = NodeFromWorldPoint(Player.position);

            foreach(Node n in m_grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                if (Path != null)
                    if (Path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (m_nodeDiameter - 0.1f));
            }
        }
    }
}
