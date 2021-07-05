using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NodeObject : MonoBehaviour
{
	public MeshRenderer rnd;
	public void ApplyColor(int index)
	{
		if (rnd == null) return;
		rnd.material.SetInt("Tile_Index", index);
	}

	private void OnEnable()
	{
		Material mat = new Material(rnd.sharedMaterial);
		rnd.material = mat;
	}
}
