using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
	public class NodeGrid
	{
		public Node[,] NodeArray;
		public Vector2Int GridSize = new Vector2Int();

		public NodeGrid(Vector2Int gridSize)
		{
			GridSize = gridSize;
			NodeArray = new Node[GridSize.x, GridSize.y];
		}

		public int MaxSize => GridSize.x * GridSize.y;

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

					if (checkX >= 0 && checkX < GridSize.x && checkY >= 0 && checkY < GridSize.y)
					{
						neighbours.Add(NodeArray[checkX, checkY]);
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

					if (checkX >= 0 && checkX < GridSize.x && checkY >= 0 && checkY < GridSize.y)
					{
						neighbours.Add(NodeArray[checkX, checkY]);
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

					if (checkX >= 0 && checkX < GridSize.x && checkY >= 0 && checkY < GridSize.y)
					{
						Node item = NodeArray[checkX, checkY];
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

					int checkX = center.gridPositionX + x;
					int checkY = center.gridPositionY + y;

					if (checkX >= 0 && checkX < GridSize.x && checkY >= 0 && checkY < GridSize.y)
					{
						neighbours.Add(NodeArray[checkX, checkY]);
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
			float percentX = (worldPosition.x + GridSize.x / 2) / GridSize.x;
			float percentY = (worldPosition.z + GridSize.y / 2) / GridSize.y;
			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);

			int x = Mathf.RoundToInt((GridSize.x - 1) * percentX);
			int y = Mathf.RoundToInt((GridSize.y - 1) * percentY);
			return NodeArray[x, y];
		}
	}
}
