using UnityEngine;

namespace Raspberry.Movement
{
	public class Traversal_Teleport : ITraversal_Method
	{
		public GameObject self;

		public Traversal_Teleport(GameObject self)
		{
			this.self = self;
		}
		public void Set_Destination(Vector3 position)
		{
			self.transform.position = position;
		}
	}
}