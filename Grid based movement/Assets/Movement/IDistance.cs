using Raspberry.Movement.Target;
using UnityEngine;

namespace Raspberry.Movement
{
	public interface IDistance
	{
		float Distance(Vector3 t1, Vector3 t2);
		bool Is_Within_Distance(ITarget self, ITarget target);
		float Effective_Range();

		void Set_Self_Position(Vector3 position);
		void Set_Target_Position(Vector3 position);
	}
}