using Library.Rotation;
using Raspberry.Movement;
using Raspberry.Movement.Target;
using UnityEngine;

namespace Assets.Movement
{
	public class Rotation_Handler : IRotate
	{
		GameObject self;
		ITarget self_Target;
		public Rotation_Handler(GameObject self, ITarget self_Target)
		{
			this.self = self;
			this.self_Target = self_Target;
		}

		public void Face_Target(ITarget target)
		{
			if (self_Target == target) return;
			target.Face_Target(this);
		}
		public void Face_Target(Vector3 targetPosition)
		{
			Rotation_Library.Face_Target(self.transform, targetPosition);
		}

		public bool Is_Facing_Target(ITarget target)
		{
			return target.Is_Facing_Target(this);
		}
		public bool Is_Facing_Target(Vector3 targetPosition)
		{
			return Rotation_Library.Is_Facing_Target(self.transform, targetPosition);
		}
	}
}
