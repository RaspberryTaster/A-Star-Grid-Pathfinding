using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tools.Object_Pooling
{
	[CreateAssetMenu(fileName = "Object Pool/ Default")]
	public class Object_Pool : ScriptableObject
	{
		public List<Object_Pool_Item> ItemsToPool;

		public void Pool_Items_To_Pooled_Objects(Transform transform, Object_Pool_Component object_Pool_Component)
		{
			for (int i = 0; i < ItemsToPool.Count; i++)
			{
				object_Pool_Component.ObjectPoolItemToPooledObject(transform, i);
			}
		}

		public bool Pool_Contains_Object(GameObject gameObject)
		{
			for (int i = 0; i < ItemsToPool.Count; i++)
			{
				if (ItemsToPool[i].objectToPool == gameObject)
				{
					return true;
				}
			}

			return false;
		}
	}


}
