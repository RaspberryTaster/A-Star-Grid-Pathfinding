using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeComponent : MonoBehaviour
{
	public Node node;
	private new Renderer renderer;
	private Color selected = Color.green;
	private Color unwalkable = Color.red;
	private Color walkable = Color.white;

	private void Awake()
	{
		renderer = GetComponent<Renderer>();
	}

	public void Set(Node node)
	{
		this.node = node;
		if(node.selected)
		{
			renderer.material.color = selected;
		}
		else
		{
			renderer.material.color = this.node.walkable ? walkable : unwalkable;
		}

	}
}
