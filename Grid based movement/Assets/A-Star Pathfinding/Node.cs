using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node : IHeapItem<Node>
{
	public bool selected;
	public bool walkable;
	public Vector3 worldPosition;
	public int gridPositionX;
	public int gridPositionY;

	public int gCost;
	public int hCost;
	public Node parent;
	int heapIndex;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridPositionX = _gridX;
		gridPositionY = _gridY;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public int HeapIndex
	{
		get
		{
			return heapIndex;
		}
		set
		{
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare)
	{
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0)
		{
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}

	public string Designation()
	{
		return $"World position: [{worldPosition}], Grid position: [{gridPositionX}, {gridPositionY}]";
	}
}