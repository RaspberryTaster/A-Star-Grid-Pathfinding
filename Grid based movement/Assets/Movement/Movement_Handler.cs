using Raspberry.Movement.Target;
using UnityEngine;
using UnityEngine.AI;

namespace Raspberry.Movement
{
	public class Movement_Handler : IMovement, IChange_Traversal
	{
		private IMovement current_State;
		public NavMeshAgent NavMeshAgent;

		public Movement_Handler(IMovement current_State, NavMeshAgent navMeshAgent)
		{
			this.current_State = current_State;
			NavMeshAgent = navMeshAgent;
		}

		public void Idle()
		{
			Change_Strategy(new Movement_Idle(this));
			current_State.Idle();
		}

		public void Change_Strategy(IMovement movement)
		{
			current_State = movement;
		}

		public void Change_Strategy(ITraversal_Method traversal_Method)
		{
			if (current_State is IChange_Traversal has_Traversal_Method)
			{
				has_Traversal_Method.Change_Strategy(traversal_Method);
			}

		}

		public void Set_Destination(ITarget target, ITraversal_Method traversal_Method)
		{
			Change_Strategy(new Movement_Move(this, traversal_Method));
			Set_Destination(target);
		}

		public void Set_Destination(ITarget target)
		{
			current_State.Set_Destination(target);
		}

		public void Set_Destination(Vector3 postion)
		{
			current_State.Set_Destination(postion);
		}
	}
}