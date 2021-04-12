﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Movement.Actions
{
	public interface IAction : IStart, IUpdate, IExit
	{
		bool IsDone();
	}
}