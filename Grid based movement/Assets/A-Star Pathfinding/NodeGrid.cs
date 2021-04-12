using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
	public class NodeGrid : MonoBehaviour
	{
		public bool displayGridGizmos;
		public LayerMask unwalkableMask;
		public Vector2 gridWorldSize;
		public float nodeRadius;
		Node[,] grid;

		public float nodeDiameter => nodeRadius * 2;

		int gridSizeX, gridSizeY;
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
			CreateGrid();
		}

		private void CreateGrid()
		{
			grid = new Node[gridSizeX, gridSizeY];
			Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

			for (int x = 0; x < gridSizeX; x++)
			{
				for (int y = 0; y < gridSizeY; y++)
				{
					Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
					bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

					worldPoint.y += quadOffset;
					grid[x, y] = new Node(walkable, worldPoint, x, y);
				}
			}
		}

		public List<Node> GetNeighbours(Node node)
		{
			List<Node> neighbours = new List<Node>();

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
						continue;

					int checkX = node.gridPositionX + x;
					int checkY = node.gridPositionY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					{
						neighbours.Add(grid[checkX, checkY]);
					}
				}
			}

			return neighbours;
		}

		public List<Node> GetNeighbours(Node node, int xAxis, int yAxis)
		{
			List<Node> neighbours = new List<Node>();

			for (int x = -xAxis; x <= xAxis; x++)
			{
				for (int y = -yAxis; y <= yAxis; y++)
				{
					if (x == 0 && y == 0)
						continue;

					int checkX = node.gridPositionX + x;
					int checkY = node.gridPositionY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					{
						neighbours.Add(grid[checkX, checkY]);
					}
				}
			}

			return neighbours;
		}

		public List<Node> GetNeighbours(Node node, List<Node> avoid)
		{
			List<Node> neighbours = new List<Node>();

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
						continue;

					int checkX = node.gridPositionX + x;
					int checkY = node.gridPositionY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					{
						Node item = grid[checkX, checkY];
						if (avoid.Contains(item)) continue;
						neighbours.Add(item);
					}
				}
			}

			return neighbours;
		}

		public List<Node> GetAdjacentNodes(Node center, bool IncludeMiddle)
		{
			List<Node> neighbours = new List<Node>();

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0 && !IncludeMiddle)
						continue;
					if (x == -1 && y == -1)
						continue;
					if (x == 1 && y == 1)
						continue;
					if (x == 1 && y == -1)
						continue;
					if (x == -1 && y == 1)
						continue;

					int checkX = (center.gridPositionX) + x;
					int checkY = (center.gridPositionY) + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					{
						neighbours.Add(grid[checkX, checkY]);
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
			float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);

			int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
			int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
			return grid[x, y];
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
			Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
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
