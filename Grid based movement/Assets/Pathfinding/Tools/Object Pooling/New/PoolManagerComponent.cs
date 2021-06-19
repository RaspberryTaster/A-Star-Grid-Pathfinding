using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerComponent : MonoBehaviour
{
	public List<PoolManager> PoolManagers;
    public PoolManager PoolManager;
    public Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();
    public List<Transform> children;
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < PoolManagers.Count; i++)
		{
			PoolManagers[i].CreatePool(poolDictionary, transform);
			Transform[] children1 = GetComponentsInChildren<Transform>();
			for (int x = 0; x < children1.Length; x++)
			{
				if (children1[x] != transform)
				{
					this.children.Add(children1[x]);
				}

			}
		}
    }

    public List<Transform> grandChildrem;

	public GameObject SpawnObject(Vector3 position, Quaternion rotation, int childIndex)
	{
		Transform child = children[childIndex];
		Transform[] tempGrandChildren = child.GetComponentsInChildren<Transform>(true);
		List<Transform> grandChildren = new List<Transform>();
		for (int i = 0; i < tempGrandChildren.Length; i++)
		{
			if (tempGrandChildren[i] != child)
			{
				grandChildren.Add(tempGrandChildren[i]);
			}
		}
		grandChildrem = grandChildren;
		return PoolManager.UseObject(poolDictionary, grandChildren, position, rotation);
	}
}
