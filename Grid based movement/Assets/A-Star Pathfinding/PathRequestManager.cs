using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour
{

	Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;

	static PathRequestManager instance;
	Pathfinding pathfinding;

	bool isProcessingPath;

	void Awake()
	{
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
	}

	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, int stoppingDistance, Action<Vector3[], bool> callback)
	{
		PathRequest newRequest = new PathRequest(pathStart, pathEnd, stoppingDistance, callback);
		instance.pathRequestQueue.Enqueue(newRequest);
		instance.TryProcessNext();
	}

	void TryProcessNext()
	{
		if (!isProcessingPath && pathRequestQueue.Count > 0)
		{
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessingPath = true;
			pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, currentPathRequest.stoppingDistance);
		}
	}

	public void FinishedProcessingPath(Vector3[] path, bool success)
	{
		currentPathRequest.callback(path, success);
		isProcessingPath = false;
		TryProcessNext();
	}
}
struct PathRequest
{
	public Vector3 pathStart;
	public Vector3 pathEnd;
	public int stoppingDistance;
	public Action<Vector3[], bool> callback;

	public PathRequest(Vector3 pathStart, Vector3 pathEnd, int stoppingDistance, Action<Vector3[], bool> callback)
	{
		this.pathStart = pathStart;
		this.pathEnd = pathEnd;
		this.stoppingDistance = stoppingDistance;
		this.callback = callback;
	}
}