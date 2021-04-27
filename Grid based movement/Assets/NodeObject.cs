using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeObject : MonoBehaviour
{
	[SerializeField] MeshRenderer rnd;
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
	}
}
