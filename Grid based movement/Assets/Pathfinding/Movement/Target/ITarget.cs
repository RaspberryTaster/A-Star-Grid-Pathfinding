using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Raspberry.Movement.Target
{
	public interface ITarget : ITo_String
	{
		void Face_Target(IRotate rotate);
		bool Is_Facing_Target(IRotate rotate);
		void Set_Range_Values(ref IRange range_Values);
		void Set_Destination(IMovement movement);
		void Set_Self_Position(IDistance distance);
		void Set_Target_Position(IDistance distance);
	}
}