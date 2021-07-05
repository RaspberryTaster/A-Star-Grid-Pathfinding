using UnityEngine;

namespace Raspberry.Movement
{
	public class Targetable : ITargetable
	{
		public GameObject self;

		public Targetable(GameObject self)
		{
			this.self = self;
		}

		public void Targeted_For_Destination(Movement_Input playerInput, IRange range, ITraversal_Method traversal_Method)
		{
			playerInput.SetObjectDestination(self, range, traversal_Method);
		}
	}
}
