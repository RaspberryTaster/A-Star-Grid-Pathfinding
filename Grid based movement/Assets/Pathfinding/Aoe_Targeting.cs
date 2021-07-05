using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aoe_Targeting : MonoBehaviour
{
    public List<Node> nodes;
	public void OnTriggerEnter(Collider other)
	{
		Node node = other.GetComponent<Node>();
		if (node == null) return;
		nodes.Add(node);
		//node.TargetSelect();
	}

	public void OnTriggerExit(Collider other)
	{
		Node node = other.GetComponent<Node>();
		if (node == null) return;
		if (nodes.Contains(node))
		{
			//node.Set();
			nodes.Remove(node);
		}
	}
}
