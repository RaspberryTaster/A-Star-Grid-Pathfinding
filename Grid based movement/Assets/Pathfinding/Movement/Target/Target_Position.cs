using UnityEngine;

namespace Raspberry.Movement.Target
{
	public class Target_Position : ITarget, IHave_Position
	{
		private Vector3 position;
		private IRange range_Values;

		public Target_Position(Vector3 position, IRange range_Values)
		{
			this.position = position;
			this.range_Values = range_Values;
		}

		public void Face_Target(IRotate rotate)
		{
			rotate.Face_Target(Get_Position());
		}

		public bool Is_Facing_Target(IRotate rotate)
		{
			return rotate.Is_Facing_Target(Get_Position());
		}
		public void Set_Destination(IMovement movement)
		{
			movement.Set_Destination(Get_Position());
		}

		public Vector3 Get_Position()
		{
			return position;
		}

		public void Set_Range_Values(ref IRange range_Values)
		{
			range_Values = this.range_Values;
		}

		public string To_String()
		{
			return "Position: " + Get_Position();
		}

		public void Set_Self_Position(IDistance distance)
		{
			distance.Set_Self_Position(Get_Position());
		}

		public void Set_Target_Position(IDistance distance)
		{
			distance.Set_Target_Position(Get_Position());
		}
	}
}