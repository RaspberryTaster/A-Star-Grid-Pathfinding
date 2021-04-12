using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Movement.Actions
{
	public interface IQueue : IDequeue
	{
		void Add(IAction action, Action_Types action_Types);
		void Action_Start(IAction action);
		void Action_Update(IAction action);
		void Delay_Action_With_Action(IAction delayedAction, IAction action, Action_Types action_Types);
		void Insert_To_Action_Queue(IAction action, Action_Types action_Types, int actionIndex);
		void Start_First_In_Line();
		void Start_If_First_In_Line(IAction action);
	}
}
