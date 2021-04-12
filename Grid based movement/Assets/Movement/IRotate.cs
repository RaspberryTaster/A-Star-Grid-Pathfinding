using Raspberry.Movement.Target;
using UnityEngine;

namespace Raspberry.Movement
{
	public interface IRotate
	{
		bool Is_Facing_Target(ITarget target);
		void Face_Target(ITarget target);
		void Face_Target(Vector3 targetPosition);
		bool Is_Facing_Target(Vector3 targetPosition);
	}
}
