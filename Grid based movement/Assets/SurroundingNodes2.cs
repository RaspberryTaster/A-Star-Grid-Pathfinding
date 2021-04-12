using Assets;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingNodes2 : MonoBehaviour
{
	public Pathfinding pathfinding;
	public NodeGrid grid;
	public int minimumCount;
	public int maximumCount;
	public Transform target;
	public Transform unit;
	public List<Node> withinRangeNodes = new List<Node>();
	public bool gizmoGrid;
	public delegate void AddNeighbour(Node node);
	public AddNeighbour OnAddNeighbour;

	public void SetUp(Transform unit, Node targetNode,int minimumRange, int maximumRange)
	{
		withinRangeNodes.Clear();
		withinRangeNodes = grid.GetWithinRange(targetNode, minimumRange, maximumRange);
		closestNode = ClosestNode(grid.NodeFromWorldPoint(unit.position), targetNode);
	}
	[Button("Search Closest")]
	public void SearchClosest()
	{
		closestNode = ClosestNode(grid.NodeFromWorldPoint(unit.position), grid.NodeFromWorldPoint(target.position));
	}
	[Button("Search")]
	public void Search()
	{
		withinRangeNodes.Clear();
		withinRangeNodes = grid.GetWithinRange(grid.NodeFromWorldPoint(target.position), minimumCount, maximumCount);
	}

	public Node closestNode;
	public Node ClosestNode(Node seeker, Node target)
	{
		Node closest = null;
		int closestDistance = 0;
		for (int i = 0; i < withinRangeNodes.Count; i++)
		{
			if(closest == null)
			{
				SetClosest(withinRangeNodes[i], pathfinding.GetDistance(withinRangeNodes[i], seeker));
			}
			else
			{
				int distanceToNode = pathfinding.GetDistance(withinRangeNodes[i], seeker);
				if (closestDistance > distanceToNode)
				{
					SetClosest(withinRangeNodes[i], distanceToNode);
				}
			}
		}

		return closest;
		void SetClosest(Node node, int distance)
		{
			if(closestDistance == distance)
			{
				int distanceFromClosest = pathfinding.GetDistance(closest, target);
				int distanceFromNewest = pathfinding.GetDistance(node, target);
				if (distanceFromClosest > distanceFromNewest) return;
			}
			closest = node;
			closestDistance = distance;
		}
	}

	void OnDrawGizmos()
	{
		if (gizmoGrid == false) return;

		if (withinRangeNodes != null)
		{
			foreach (Node n in withinRangeNodes)
			{
				Gizmos.color = n == closestNode ? Color.blue : Color.red;
				Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (grid.nodeDiameter - .3f));
			}

		}
	}
}
