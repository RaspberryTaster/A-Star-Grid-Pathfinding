using Raspberry.Library.Movement;
using Raspberry.Movement.Actions;
using Raspberry.Movement.Target;
using UnityEngine;
using UnityEngine.AI;

namespace Raspberry.Movement
{
	[RequireComponent(typeof(StateMachine))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class Movement_Component : MonoBehaviour
	{
		public ITarget self;
		public Range_Component Range_Component;
		public Movement_Handler Movement_Handler;
		public NavMeshAgent NavMeshAgent;
		public Distance_Limiter Distance_Limiter;
		private bool hasStopped;
		public StateMachine queue_Component;

		private void Awake()
		{
			queue_Component = GetComponent<StateMachine>();
			Distance_Limiter = GetComponent<Distance_Limiter>();
		}
		void Start()
		{
			IRange range_Values = Range_Component;
			self = new Target_GameObject(gameObject, range_Values);
			Movement_Handler = new Movement_Handler(new Movement_Idle(Movement_Handler), NavMeshAgent);
		}

		void Update()
		{
			if (Distance_Limiter != null)
			{
				if (Distance_Limiter.Reached_Max_Distance)
				{
					Stop();
				}

			}

			if (Movement_Library.NavMesh_Stopped(NavMeshAgent) && hasStopped)
			{
				Stop();
			}
			else
			{
				hasStopped = false;
			}

		}

		private void Stop()
		{
			hasStopped = true;
			Movement_Handler.Idle();
		}

		public void Move(ITarget target, ITraversal_Method traversal_Method, IMove_Intent move_Intent, Action_Types action_Types)
		{
			//IAction action = new Action_Dependant_On_Move(queue_Component, Movement_Handler, target, traversal_Method, self, move_Intent, gameObject);
			//queue_Component.Dequeue_All_Before_Adding_Action(action, action_Types + queue_Component.actionCount);
		}
	}
}
