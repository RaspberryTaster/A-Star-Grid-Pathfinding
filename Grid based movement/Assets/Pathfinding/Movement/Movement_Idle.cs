using Raspberry.Library.Movement;
using Raspberry.Movement.Target;
using UnityEngine;

namespace Raspberry.Movement
{
	public class Movement_Idle : IMovement
	{
		private Movement_Handler movement_Manager;

		public Movement_Idle(Movement_Handler movement_Manager)
		{
			this.movement_Manager = movement_Manager;
		}

		public void Idle()
		{
			Movement_Library.NavMesh_Stop(movement_Manager.NavMeshAgent);
		}

		public void Set_Destination(ITarget target)
		{

		}

		public void Set_Destination(Vector3 position)
		{

		}

		public void Set_Destination(ITarget target, ITraversal_Method traversal_Method)
		{
			movement_Manager.Set_Destination(target, traversal_Method);
		}
	}
}
