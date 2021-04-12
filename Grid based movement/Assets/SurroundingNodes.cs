using Assets;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingNodes : MonoBehaviour
{
	public NodeGrid grid;
	public Pathfinding Pathfinding;
	public List<Node> surroundingNodes = new List<Node>();
	public List<Node> diagonalNodes = new List<Node>();
	public int moves = 10;
	public int xAxis;
	public int yAxis;
	public Transform theTarget;
	public Transform unit;
	[Button("Aura")]
	public void Aura()
	{
		//SetUp();
	}

	public void SetUp(Transform target, Transform unit)
	{
		theTarget = target;
		this.unit = unit;

		Node center = grid.NodeFromWorldPoint(theTarget.position);
		Node seeker = grid.NodeFromWorldPoint(unit.transform.position);
		SetAura(center);
		ClosestNode(seeker);
	}

	private void SetAura(Node center)
	{
		surroundingNodes.Clear();

		List<Node> neightbours = grid.GetNeighbours(center, xAxis, yAxis);
		List<Node> outerNeighbours = new List<Node>();
		for (int i = 0; i < neightbours.Count; i++)
		{
			if (neightbours[i].walkable && !surroundingNodes.Contains(neightbours[i]))
			{
				int distance = Pathfinding.GetDistance(center, neightbours[i]);
				if (distance % 14 == 0)
				{
					diagonalNodes.Add(neightbours[i]);
				}
				else
				{
					surroundingNodes.Add(neightbours[i]);
				}

				outerNeighbours.Add(neightbours[i]);
			}
		}
	}


	public void SetRange()
	{
		Node center = grid.NodeFromWorldPoint(theTarget.position);
		List<Node> neighbours = grid.GetNeighbours(center, xAxis, yAxis);
	}
	public Node closestNode;
	public Node ClosestNode(Node seeker)
	{
		Node closest = null;
		int closestDistance = 0;
		for (int i = 0; i < surroundingNodes.Count; i++)
		{
			if(closest == null)
			{
				SetClosest(surroundingNodes[i], Pathfinding.GetDistance(surroundingNodes[i], seeker));
			}
			else
			{
				int distanceToNode = Pathfinding.GetDistance(surroundingNodes[i], seeker);
				if (closestDistance > distanceToNode)
				{
					SetClosest(surroundingNodes[i], distanceToNode);
				}
			}
		}

		this.closestNode = closest;
		return closest;
		void SetClosest(Node currentNode, int _closestDistance)
		{
			closest = currentNode;
			closestDistance = _closestDistance;
		}
	}

	[SerializeField] private float gizmoHeight = 0.3f;
	[SerializeField] private float gizmoBoundry = -.1f;
	[SerializeField] private float nodeRadius = 0.7f;

	public float NodeDiameter
	{
		get
		{
			return nodeRadius * 2;
		}
	}

	private void OnDrawGizmos()
	{
		foreach (Node n in surroundingNodes)
		{
			Gizmos.color = n == closestNode? Color.yellow : Color.magenta;
			Vector3 size = Vector3.one * (NodeDiameter - gizmoBoundry);
			size.y = gizmoHeight;
			Gizmos.DrawWireCube(n.worldPosition, size);
		}
		foreach(Node n in diagonalNodes)
		{
			if (diagonalNodes.Contains(n))
			{
				Gizmos.color = Color.green;
			}
			Vector3 size = Vector3.one * (NodeDiameter - gizmoBoundry);
			size.y = gizmoHeight;
			Gizmos.DrawWireCube(n.worldPosition, size);
		}
	}
}
