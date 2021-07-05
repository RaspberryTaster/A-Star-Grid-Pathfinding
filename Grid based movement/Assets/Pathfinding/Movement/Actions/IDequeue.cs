using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Movement.Actions
{
	public interface IDequeue
	{
		void Dequeue(IAction action);
		void Dequeue_All();
	}
}
