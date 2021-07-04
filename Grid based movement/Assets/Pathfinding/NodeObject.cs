using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NodeObject : MonoBehaviour
{
	public MeshRenderer rnd;
<<<<<<< Updated upstream
	MaterialPropertyBlock mpb;
	static readonly int shPropColor = Shader.PropertyToID("_Color");
	public MaterialPropertyBlock Mpb
	{
		get
		{
			if (mpb == null)
				mpb = new MaterialPropertyBlock();
			return mpb;
		}
	}
	public void ApplyColor(Color color)
	{
		if (rnd == null) return;
		rnd.material.SetColor(shPropColor, color);
		rnd.SetPropertyBlock(Mpb);
=======
	public void ApplyColor(int index)
	{
		if (rnd == null) return;
		rnd.material.SetInt("Tile_Index", index);
>>>>>>> Stashed changes
	}

	private void OnEnable()
	{
		Material mat = new Material(rnd.sharedMaterial);
		rnd.material = mat;
	}

	private void OnDisable()
	{

	}
}
