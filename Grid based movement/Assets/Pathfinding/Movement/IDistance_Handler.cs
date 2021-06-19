using Raspberry.Movement.Target;
using UnityEngine;

namespace Raspberry.Movement
{
	public class Distance_Handler : IDistance
	{
		private ITarget self;
		private ITarget target;
		private Vector3 self_Position;
		private Vector3 target_Position;
		private IMove_Intent move_Intent;

		public Distance_Handler(ITarget self, ITarget target, IMove_Intent move_Intent)
		{
			Set_Targets(self, target);
			this.move_Intent = move_Intent;
		}

		public float Distance(Vector3 t1, Vector3 t2)
		{
			return Vector3.Distance(t1, t2);
		}

		public bool Is_Within_Distance(ITarget self, ITarget target)
		{
			Set_Targets(self, target);

			this.target.Set_Target_Position(this);
			this.self.Set_Self_Position(this);

			float distance = Distance(self_Position, target_Position);
			float effective_Range = Effective_Range();
			bool Is_Within_Distance = distance <= effective_Range;
			//Debug.Log("Distance: " + distance + ", effective range: " + effective_Range);
			return Is_Within_Distance;
		}

		private void Set_Targets(ITarget self, ITarget target)
		{
			this.self = self;
			this.target = target;
		}

		public float Effective_Range()
		{
			float value = move_Intent.Effective_Range();
			return value;
		}

		public void Set_Self_Position(Vector3 position)
		{
			self_Position = position;
		}
		public void Set_Target_Position(Vector3 position)
		{
			target_Position = position;
		}
	}
}