using Assets;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatComponent))]
public class UnitMovement : MonoBehaviour
{ 
	public int MoveDistance;
	private Pathfinding Pathfinding;
	private SquareGrid grid;
	private CombatComponent combatComponent;

	private void Awake()
	{
		if(grid == null)
		{
			grid = FindObjectOfType<SquareGrid>();
		}
		if(Pathfinding == null)
		{
			Pathfinding = FindObjectOfType<Pathfinding>();
		}
		combatComponent = GetComponent<CombatComponent>();
	}

	private void Start()
	{
		GetDistanceNodes();
	}

	[Button]
	public void GetDistanceNodes()
	{
		foreach (Node n in WithinRangeNodes)
		{
			n.SetColor(n.DefaultNodeIndex);
		}
		foreach (Node n in MovementNodes)
		{
			n.SetColor(n.DefaultNodeIndex);
		}

		MovementNodes = PathfindDistance(grid.NodeGrid.NodeFromWorldPoint(transform.position));
		WithinRangeNodes = PredictedRangeNodes(MovementNodes);
		foreach (Node n in WithinRangeNodes)
		{
			n.SetColor((int)TIleMode.ATTACKRANGE);
		}
		foreach (Node n in MovementNodes)
		{
			n.SetColor((int)TIleMode.MOVEMENT);
		}
	}

	public List<Node> MovementNodes = new List<Node>();
	public List<Node> WithinRangeNodes = new List<Node>();

	public List<Node> PathfindDistance(Node center)
	{
		Queue<Node> frontier = new Queue<Node>();
		List<Node> reached = new List<Node>() { center };
		frontier.Enqueue(center);
		while(frontier.Count != 0)
		{
			Node current = frontier.Dequeue();
			List<Node> neighbours = grid.NodeGrid.GetNeighbours(current);
			for(int i = 0; i < neighbours.Count; i ++)
			{
				Node next = neighbours[i];
				int distance = Pathfinding.GetDistance(center, next);
				bool notWithinDistance = distance > MoveDistance;
				if (reached.Contains(next) || notWithinDistance || !next.walkable) continue;
				frontier.Enqueue(next);
				reached.Add(next);
			}
		}

		return reached;
	}

	public List<Node> PredictedRangeNodes(List<Node> targetNodes)
	{
		List<Node> value = new List<Node>();
		foreach(Node n in targetNodes)
		{
			List<Node> rangeNodes = grid.NodeGrid.GetWithinRange(n, combatComponent.minAttackRange, combatComponent.maxAttackRange);
			for(int i = 0; i < rangeNodes.Count; i++)
			{
				if (value.Contains(rangeNodes[i])) continue;
				value.Add(rangeNodes[i]);
			}
		}

		 return value;
	}

	public bool DrawGizmos;
	private void OnDrawGizmos()
	{
		if (DrawGizmos == false) return;
		MovementGizmos();
	}

	private void MovementGizmos()
	{
		foreach (Node n in MovementNodes)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (grid.NodeDiameter - .3f));
		}
		foreach (Node n in WithinRangeNodes)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (grid.NodeDiameter - .4f));
		}
	}
}
