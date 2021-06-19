using Raspberry.Movement;
using Raspberry.Movement.Target;
using UnityEngine;

namespace Raspberry.Movement
{
	public class Movement_Move : IMovement, IChange_Traversal, IChange_Movement
	{
		Movement_Handler movement_Manager;
		public ITraversal_Method cur_Traversal_Method = new Traversal_Null();

		public Movement_Move(Movement_Handler movement_Manager, ITraversal_Method cur_Traversal_Method)
		{
			this.movement_Manager = movement_Manager;
			this.cur_Traversal_Method = cur_Traversal_Method;
		}

		public Movement_Move(Movement_Handler movement_Manager)
		{
			this.movement_Manager = movement_Manager;
		}

		public void Change_Strategy(IMovement movement)
		{
			movement_Manager.Change_Strategy(movement);
		}

		public void Idle()
		{
			movement_Manager.Idle();
		}

		public void Set_Destination(ITarget target)
		{
			target.Set_Destination(this);
		}

		public void Set_Destination(Vector3 postion)
		{
			cur_Traversal_Method.Set_Destination(postion);
		}

		public void Change_Strategy(ITraversal_Method traversal_Method)
		{
			cur_Traversal_Method = traversal_Method;
		}

		public void Set_Destination(ITarget target, ITraversal_Method traversal_Method)
		{
			movement_Manager.Set_Destination(target, traversal_Method);
		}
	}
}
