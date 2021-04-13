using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
	public class NodeGrid : MonoBehaviour
	{
		public bool displayGridGizmos;
		public LayerMask unwalkableMask;
		public Vector3 gridWorldSize;
		public float nodeRadius;
		Node[,,] grid;

		public float nodeDiameter => nodeRadius * 2;

		int gridSizeX, gridSizeY, gridSizeZ;
		public int MaxSize => gridSizeX * gridSizeY;
		public float quadOffset;
		void Awake()
		{
			SetAndCreateGrid();
		}

		[Button("Create grid")]
		private void SetAndCreateGrid()
		{
			gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
			gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
			gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);
			CreateGrid();
		}

		private void CreateGrid()
		{
			grid = new Node[gridSizeX, gridSizeY, gridSizeZ];
			Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2 - Vector3.forward * gridWorldSize.z /2;

			for(int y = 0; y < gridSizeY; y++)
			{
				for (int x = 0; x < gridSizeX; x++)
				{
					for (int z = 0; z < gridSizeZ; z++)
					{
						Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
						bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

						worldPoint.y += quadOffset;
						grid[x, y, z] = new Node(walkable, worldPoint, x, y, z);
					}
				}
			}

		}

		public List<Node> GetNeighbours(Node node)
		{
			List<Node> neighbours = new List<Node>();
			for(int y = -1; y <= 1; y++)
			{
				for (int x = -1; x <= 1; x++)
				{
					for (int z = -1; z <= 1; z++)
					{
						if (x == 0 && z == 0 && y == 0)
							continue;

						int checkX = node.gridPositionX + x;
						int checkY = node.gridPositionY + y;
						int checkZ = node.gridPositionZ + z;
						if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && checkZ >= 0 && checkZ < gridSizeZ)
						{
							neighbours.Add(grid[checkX, checkY, checkZ]);
						}
					}
				}
			}


			return neighbours;
		}

		public List<Node> GetNeighbours(Node node, int xAxis, int yAxis, int zAxis)
		{
			List<Node> neighbours = new List<Node>();

			for (int y = -yAxis; y <= yAxis; y++)
			{
				for (int x = -xAxis; x <= xAxis; x++)
				{
					for (int z = -zAxis; z <= zAxis; z++)
					{
						if (x == 0 && z == 0 && y == 0)
							continue;

						int checkX = node.gridPositionX + x;
						int checkY = node.gridPositionY + y;
						int checkZ = node.gridPositionZ + z;
						if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && checkZ >= 0 && checkZ < gridSizeZ)
						{
							neighbours.Add(grid[checkX, checkY, checkZ]);
						}
					}
				}
			}


			return neighbours;
		}

		public List<Node> GetNeighbours(Node node, List<Node> avoid)
		{
			List<Node> neighbours = new List<Node>();

			for (int y = -1; y <= 1; y++)
			{
				for (int x = -1; x <= 1; x++)
				{
					for (int z = -1; z <= 1; z++)
					{
						if (x == 0 && z == 0 && y == 0)
							continue;

						int checkX = node.gridPositionX + x;
						int checkY = node.gridPositionY + y;
						int checkZ = node.gridPositionZ + z;
						if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && checkZ >= 0 && checkZ < gridSizeZ)
						{
							Node _node = grid[checkX, checkY,checkZ];
							if (avoid.Contains(_node)) continue;
							neighbours.Add(_node);
						}
					}
				}
			}

			return neighbours;
		}

		public List<Node> GetAdjacentNodes(Node center, bool IncludeMiddle)
		{
			List<Node> neighbours = new List<Node>();

			for (int y = -1; y <= 1; y++)
			{
				for (int x = -1; x <= 1; x++)
				{
					for (int z = -1; z <= 1; z++)
					{
						if (x == 0 && z == 0 && y == 9 && !IncludeMiddle)
							continue;
						if (x == 1 && y== 0 && z == 1)
							continue;
						if (x == 1 && y == 0 && z == -1)
							continue;
						if (x == -1 && y == 0 && z == 1)
							continue;
						if (x == -1 && y == 0 && z == -1)
							continue;
						if (x == 1 && y == 1 && z == 1)
							continue;
						if (x == 1 && y == 1 && z == -1)
							continue;
						if (x == -1 && y == 1 && z == 1)
							continue;
						if (x == -1 && y == 1 && z == -1)
							continue;
						if (x == 1 && y == 0 && z == 1)
							continue;
						if (x == 1 && y == -1 && z == -1)
							continue;
						if (x == -1 && y == -1 && z == 1)
							continue;
						if (x == -1 && y == -1 && z == -1)
							continue;

						int checkX = (center.gridPositionX) + x;
						int checkY = (center.gridPositionY) + y;
						int checkZ = (center.gridPositionZ) + z;

						if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && checkZ >= 0 && checkZ < gridSizeZ)
						{
							Node _node = grid[checkX, checkY, checkZ];
							if (neighbours.Contains(_node))
							{
								neighbours.Add(_node);
							}
						}
					}
				}
			}
			

			return neighbours;
		}

		public List<Node> GetWithinRange(Node center, int minimumCount, int maximumCount)
		{
			List<Node> reached = new List<Node>();
			List<Node> frontier = new List<Node>();
			List<Node> withinRangeNodes = new List<Node>();
			for (int i = 0; i < maximumCount; i++)
			{
				if (i == 0)
				{
					frontier = GetAdjacentNodes(center, false);
				}
				else
				{
					List<Node> newlatestNeighbours = new List<Node>();
					for (int o = 0; o < frontier.Count; o++)
					{
						List<Node> list = GetAdjacentNodes(frontier[o], false);
						for (int p = 0; p < list.Count; p++)
						{
							if (reached.Contains(list[p]) || center == list[p] || newlatestNeighbours.Contains(list[p])) continue;
							newlatestNeighbours.Add(list[p]);
						}
					}
					frontier = newlatestNeighbours;
				}

				for (int a = 0; a < frontier.Count; a++)
				{
					if (reached.Contains(frontier[a])) continue;
					reached.Add(frontier[a]);
					if (i >= (minimumCount - 1) && minimumCount != 0)
					{
						withinRangeNodes.Add(frontier[a]);
					}
				}
			}
			return withinRangeNodes;
		}


		public Node NodeFromWorldPoint(Vector3 worldPosition)
		{
			float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
			float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
			float percentZ = (worldPosition.z + gridWorldSize.z / 2) / gridWorldSize.z;
			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);
			percentZ = Mathf.Clamp01(percentZ);
			int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
			int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
			int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);
			return grid[x, y, z];
		}


		[SerializeField] private float gizmoHeight = 0.3f;
		[SerializeField] private float gizmoBoundry = -.1f;

		void OnDrawGizmos()
		{
			DrawBoundryGizmos();
			if (grid != null && displayGridGizmos)
			{
				DrawGridGizmos();
			}
		}

		private void DrawBoundryGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, gridWorldSize.z));
		}

		private void DrawGridGizmos()
		{
			foreach (Node n in grid)
			{
				if (n == null) return;
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Vector3 size = Vector3.one * (nodeDiameter - gizmoBoundry);
				size.y = gizmoHeight;
				Gizmos.DrawWireCube(n.worldPosition, size);
			}
		}
	}
}
