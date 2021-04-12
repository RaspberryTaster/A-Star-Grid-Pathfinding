using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System;

public class Unit : MonoBehaviour
{
	public float speed = 20;
	private Vector3[] path;
	private int targetIndex;
	private Action<bool> endOfPath;
	private float yOffset;

	private void Awake()
	{
		yOffset = transform.position.y;
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

	public void OnDrawGizmos() {
		if (path != null)
		{
			DrawPathGizmos();
		}
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
}