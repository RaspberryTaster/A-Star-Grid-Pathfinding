using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PoolManager : ScriptableObject
{
	public GameObject prefab;
	public int poolSize;
	public bool Expandable;
	public void CreatePool(Dictionary<int, Queue<ObjectInstance>> poolDictionary, Transform transform)
	{
		GameObject poolHolder = new GameObject(prefab.name + " pool");
		poolHolder.transform.parent = transform;
		for (int i = 0; i < poolSize; i++)
		{
			GameObject objectInstance = Instantiate(prefab);
			ObjectInstance newObject = new ObjectInstance(objectInstance);
			int poolKey = objectInstance.GetInstanceID();
			if(!poolDictionary.ContainsKey(poolKey))
			{
				poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
			}

			poolDictionary[poolKey].Enqueue(newObject);
			newObject.SetParent(poolHolder.transform);
		}
	}

	public GameObject UseObject(Dictionary<int, Queue<ObjectInstance>> poolDictionary, List<Transform> children, Vector3 position, Quaternion rotation)
	{

		for (int i = 0; i < children.Count; i++)
		{
			int poolKey = children[i].gameObject.GetInstanceID();
			if (poolDictionary.ContainsKey(poolKey))
			{
				ObjectInstance objectToUse = poolDictionary[poolKey].Dequeue();
				poolDictionary[poolKey].Enqueue(objectToUse);
				if (!objectToUse.gameObject.activeInHierarchy)
				{
					objectToUse.Use(position, rotation);
					return objectToUse.gameObject;
				}
			}
			else
			{
				Debug.LogWarning("Pool dictionary does not contain " + children[i].name + " pool key");
			}
		}

		if(Expandable && children[0] != null)
		{
			GameObject objectInstance = Instantiate(prefab);
			ObjectInstance newObject = new ObjectInstance(objectInstance);
			int poolKey = objectInstance.GetInstanceID();
			if (!poolDictionary.ContainsKey(poolKey))
			{
				poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
			}

			poolDictionary[poolKey].Enqueue(newObject);
			newObject.SetParent(children[0].parent);
		}

		return null;
	}
	public GameObject ReuseObject(Dictionary<int, Queue<ObjectInstance>> poolDictionary, GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int poolKey = prefab.GetInstanceID();
		if(poolDictionary.ContainsKey(poolKey))
		{
			ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
			poolDictionary[poolKey].Enqueue(objectToReuse);
			objectToReuse.Reuse(position, rotation);
			return objectToReuse.gameObject;
		}

		return null;
	}
}
public class ObjectInstance
{
	public GameObject gameObject;
	private Transform transform;
	private bool hasPoolObjectComponent { get { return poolObjectComponent != null; } }
	private PoolObject poolObjectComponent;
	public ObjectInstance(GameObject objectInstance)
	{
		gameObject = objectInstance;
		gameObject.SetActive(false);
		transform = gameObject.transform;
		poolObjectComponent = gameObject.GetComponent<PoolObject>();
	}

	public void Reuse(Vector3 position, Quaternion rotation)
	{
		if (hasPoolObjectComponent)
		{
			poolObjectComponent.OnObjectReuse();
		}

		Use(position, rotation);
	}

	public void Use(Vector3 position, Quaternion rotation)
	{
		gameObject.SetActive(true);
		transform.position = position;
		transform.rotation = rotation;
	}
	public void SetParent(Transform parent)
	{
		transform.parent = parent;
	}
}
