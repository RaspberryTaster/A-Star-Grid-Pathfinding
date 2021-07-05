using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(CombatComponent))]
public class Unit : MonoBehaviour
{
	private float speed = 20;

	public int MoveDistance = 10;
	private Pathfinding Pathfinding;
	private SquareGrid grid;
	private CombatComponent combatComponent;
	
	private Vector3[] path;
	private int targetIndex;
	private Action<bool> endOfPath;
	private float yOffset;

	private void Awake()
	{
		yOffset = transform.position.y;

		if (grid == null)
		{
			grid = FindObjectOfType<SquareGrid>();
		}
		if (Pathfinding == null)
		{
			Pathfinding = FindObjectOfType<Pathfinding>();
		}
		
		combatComponent = GetComponent<CombatComponent>();
	}

	private void Start()
	{
		SetDistanceNodes();
	}

	public void Move(Vector3 targetPosition, int stoppingDistance, Action<bool> callback)
	{
		endOfPath = callback;
		PathRequestManager.RequestPath(transform.position, targetPosition, stoppingDistance, OnPathFound);
	}
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine(nameof(FollowPath));
			StartCoroutine(nameof(FollowPath));
		}
	}

	[Button]
	public void SetDistanceNodes()
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
		while (frontier.Count != 0)
		{
			Node current = frontier.Dequeue();
			List<Node> neighbours = grid.NodeGrid.GetNeighbours(current);
			for (int i = 0; i < neighbours.Count; i++)
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
		foreach (Node n in targetNodes)
		{
			List<Node> rangeNodes = grid.NodeGrid.GetWithinRange(n, combatComponent.minAttackRange, combatComponent.maxAttackRange);
			for (int i = 0; i < rangeNodes.Count; i++)
			{
				if (value.Contains(rangeNodes[i])) continue;
				value.Add(rangeNodes[i]);
			}
		}

		return value;
	}

	IEnumerator FollowPath() {
		if (path.Length == 0)
		{
			endOfPath?.Invoke(true);
			yield break;
		} 
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					endOfPath?.Invoke(true);
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}

	public bool DrawGizmos;
	public void OnDrawGizmos() {
		if (!DrawGizmos) return;
		if (path != null)
		{
			DrawPathGizmos();
		}

		MovementGizmos();
	}

	private void DrawPathGizmos()
	{
		for (int i = targetIndex; i < path.Length; i++)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawCube(path[i], Vector3.one);

			if (i == targetIndex)
			{
				Gizmos.DrawLine(transform.position, path[i]);
			}
			else
			{
				Gizmos.DrawLine(path[i - 1], path[i]);
			}
		}
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