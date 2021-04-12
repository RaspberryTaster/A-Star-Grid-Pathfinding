using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tools.Object_Pooling
{
	[Serializable]
	public class Object_Pool_Item
	{

		public GameObject objectToPool;
		public int amountToPool;
		public bool shouldExpand = true;

		public Object_Pool_Item(GameObject obj, int amt, bool exp = true)
		{
			objectToPool = obj;
			amountToPool = Mathf.Max(amt, 2);
			shouldExpand = exp;
		}
	}
}
