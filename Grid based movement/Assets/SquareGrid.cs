using Assets;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SquareGrid : MonoBehaviour
{
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float NodeRadius = 0.5f;
	public bool DrawGizmos;
	public float NodeDiameter => NodeRadius * 2;
	public NodeGrid NodeGrid;
	public PlaneGrid planeGrid;
	[SerializeField] private GameObject GridNodes;
	[SerializeField] private NodeObject NodePrefab;


	int gridSizeX, gridSizeY;

	private void OnValidate()
	{
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / NodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / NodeDiameter);
	}

	void Awake()
	{
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / NodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / NodeDiameter);

		CreateGrid();
	}

	[Button]
	void CreateGrid()
	{
		if (GridNodes != null)
		{
			DestroyImmediate(GridNodes);
		}

		GridNodes = new GameObject("Grid Nodes");
		GridNodes.transform.parent = transform;
		GridNodes.transform.position = Vector3.zero;

		NodeGrid = new NodeGrid(new Vector2Int(gridSizeX, gridSizeY));
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridSizeX/ 2 - Vector3.forward * gridSizeY / 2;

		for (int x = 0; x < NodeGrid.NodeArray.GetLength(0); x++)
		{
			for (int y = 0; y < NodeGrid.NodeArray.GetLength(1); y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.forward * (y * NodeDiameter + NodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, unwalkableMask));
				NodeObject nodeObject = Instantiate(NodePrefab, worldPoint, Quaternion.identity);
				worldPoint = planeGrid.InstantiateNodeObject(nodeObject, worldPoint);

				nodeObject.transform.parent = GridNodes.transform;
				NodeGrid.NodeArray[x, y] = new Node(walkable, worldPoint, x, y, nodeObject);
			}
		}
	}

	public float gizmoBoundry = .1f;
	public float gizmoNodeHeight = 1;
	Vector3 GizmoNodeSize
	{
		get
		{
			Vector3 gizmoSize = Vector3.one * (NodeDiameter - gizmoBoundry);
			gizmoSize.y = gizmoNodeHeight;
			return gizmoSize;
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX, gizmoNodeHeight,gridSizeY));

		if (!DrawGizmos) return;
		if (NodeGrid != null)
		{
			foreach (Node n in NodeGrid.NodeArray)
			{
				Gizmos.DrawWireCube(n.worldPosition, GizmoNodeSize);
			}
		}
	}
}
