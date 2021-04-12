using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Movement
{
	public interface IMove_Intent
	{
		void Execute();
		float Effective_Range();
	}
}
