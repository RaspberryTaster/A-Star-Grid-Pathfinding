using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node : IHeapItem<Node>
{
	public int DefaultNodeIndex;

	public bool selected;
	public bool walkable;
	public Vector3 worldPosition;
	public int gridPositionX;
	public int gridPositionY;

	public int gCost;
	public int hCost;
	public Node parent;
	public NodeObject nodeObject;
	int heapIndex;
<<<<<<< Updated upstream:Grid based movement/Assets/Pathfinding/A-Star Pathfinding/Node.cs
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, NodeObject nodeObject = null)
=======
<<<<<<< Updated upstream:Grid based movement/Assets/A-Star Pathfinding/Node.cs

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
=======
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY,int  DefaultNodeIndex, NodeObject nodeObject = null)
>>>>>>> Stashed changes:Grid based movement/Assets/Pathfinding/A-Star Pathfinding/Node.cs
>>>>>>> Stashed changes:Grid based movement/Assets/A-Star Pathfinding/Node.cs
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridPositionX = _gridX;
		gridPositionY = _gridY;
<<<<<<< Updated upstream:Grid based movement/Assets/Pathfinding/A-Star Pathfinding/Node.cs
		this.nodeObject = nodeObject;
		this.nodeObject.transform.position = worldPosition;
=======
<<<<<<< Updated upstream:Grid based movement/Assets/A-Star Pathfinding/Node.cs
=======
		this.nodeObject = nodeObject;
		this.DefaultNodeIndex = DefaultNodeIndex;
		this.nodeObject.transform.position = worldPosition;
		SetColor(DefaultNodeIndex);
>>>>>>> Stashed changes:Grid based movement/Assets/Pathfinding/A-Star Pathfinding/Node.cs
>>>>>>> Stashed changes:Grid based movement/Assets/A-Star Pathfinding/Node.cs
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
<<<<<<< Updated upstream:Grid based movement/Assets/Pathfinding/A-Star Pathfinding/Node.cs


	public void SetColor(Color color)
	{
		if (nodeObject == null) return;
		nodeObject.ApplyColor(color);
	}
=======
<<<<<<< Updated upstream:Grid based movement/Assets/A-Star Pathfinding/Node.cs
=======


	public void SetColor(int index)
	{
		if (nodeObject == null) return;
		nodeObject.ApplyColor(index);
	}
>>>>>>> Stashed changes:Grid based movement/Assets/Pathfinding/A-Star Pathfinding/Node.cs
>>>>>>> Stashed changes:Grid based movement/Assets/A-Star Pathfinding/Node.cs
}