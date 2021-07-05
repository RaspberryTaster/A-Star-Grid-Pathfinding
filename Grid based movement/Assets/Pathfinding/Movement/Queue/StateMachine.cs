using Assets.Movement;
using Raspberry.Movement.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace Raspberry.Movement
{
	public class StateMachine : MonoBehaviour, IQueue, IDequeue
	{
		public IQueue current_Strategy;
		List<IAction> actions = new List<IAction>();
		[SerializeField] List<Action_Types> actionNames = new List<Action_Types>();
		public int actionCount;

		void Start()
		{
			current_Strategy = new Queue_Standard(actions, actionNames, this);
		}


		void Update()
		{

			if (actions.Count != 0)
			{
				Action_Update(actions[0]);
			}
		}

		public void Add(IAction action, Action_Types action_Types)
		{
			current_Strategy.Add(action, action_Types);
		}


		public void Action_Start(IAction action)
		{
			current_Strategy.Action_Start(action);
		}

		public void Action_Update(IAction action)
		{
			current_Strategy.Action_Update(action);
		}

		public void Change_Strategy(IQueue queue)
		{
			current_Strategy = queue;
		}

		public void Dequeue(IAction action)
		{
			if (action == null) return;
			current_Strategy.Dequeue(action);
		}

		public void Dequeue_All()
		{
			current_Strategy.Dequeue_All();
		}
		public void Delay_Action_With_Action(IAction delayedAction, IAction action, Action_Types action_Types)
		{
			current_Strategy.Delay_Action_With_Action(delayedAction, action, action_Types);
		}

		public void Insert_To_Action_Queue(IAction action, Action_Types action_Types, int acitonIndex)
		{
			current_Strategy.Insert_To_Action_Queue(action, action_Types, acitonIndex);
		}

		public void Start_First_In_Line()
		{
			current_Strategy.Start_First_In_Line();
		}

		public void Start_If_First_In_Line(IAction action)
		{
			current_Strategy.Start_If_First_In_Line(action);
		}

		public void Dequeue_All_Before_Adding_Action(IAction action, Action_Types action_Types)
		{
			Dequeue_All();
			Add(action, action_Types);

		}
	}
}
