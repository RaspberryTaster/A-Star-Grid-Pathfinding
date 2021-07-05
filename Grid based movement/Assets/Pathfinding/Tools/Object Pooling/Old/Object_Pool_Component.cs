using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tools.Object_Pooling
{
	public class Object_Pool_Component : MonoBehaviour
	{
		public Object_Pool Object_Pool;
		private List<Object_Pool_Item> ItemsToPool => Object_Pool.ItemsToPool;
		public List<List<GameObject>> pooledObjectsList;
		public List<GameObject> pooledObjects;
		private List<int> positions;

		void Awake()
		{
			pooledObjectsList = new List<List<GameObject>>();
			pooledObjects = new List<GameObject>();
			positions = new List<int>();
			Object_Pool.Pool_Items_To_Pooled_Objects(transform, this);
		}

		public bool Pool_Contains_Object(GameObject gameObject)
		{
			return Object_Pool.Pool_Contains_Object(gameObject);
		}
		//public GameObject GetPooledObject(int whi)
		public GameObject GetPooledObject(int index)
		{
			int curSize = pooledObjectsList[index].Count;
			for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
			{

				if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
				{
					positions[index] = i % curSize;
					return pooledObjectsList[index][i % curSize];
				}
			}

			if (ItemsToPool[index].shouldExpand)
			{

				GameObject obj = Instantiate(ItemsToPool[index].objectToPool);
				obj.SetActive(false);
				obj.transform.parent = transform;
				pooledObjectsList[index].Add(obj);
				return obj;

			}
			return null;
		}

		public List<GameObject> GetAllPooledObjects(int index)
		{
			return pooledObjectsList[index];
		}


		public int AddObject(GameObject go, int amt = 3, bool exp = true)
		{
			Object_Pool_Item item = new Object_Pool_Item(go, amt, exp);
			int currLen = ItemsToPool.Count;
			ItemsToPool.Add(item);
			ObjectPoolItemToPooledObject(transform, currLen);
			return currLen;
		}


		public void ObjectPoolItemToPooledObject(Transform transform, int index)
		{
			Object_Pool_Item item = ItemsToPool[index];

			pooledObjects = new List<GameObject>();
			for (int i = 0; i < item.amountToPool; i++)
			{
				GameObject obj = Instantiate(item.objectToPool);
				obj.SetActive(false);
				obj.transform.parent = transform;
				pooledObjects.Add(obj);
			}
			pooledObjectsList.Add(pooledObjects);
			positions.Add(0);

		}
	}
}
