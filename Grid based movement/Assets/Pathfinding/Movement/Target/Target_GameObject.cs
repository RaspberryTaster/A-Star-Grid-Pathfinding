using Assets.Movement.Target;
using Raspberry.Movement.Target;
using UnityEngine;

namespace Raspberry.Movement
{
	public class Target_GameObject : ITarget, IHave_Position, IHave_GameObject
	{
		private GameObject gameObject;
		private IRange range_Values;

		public Target_GameObject(GameObject gameObject, IRange range_Values)
		{
			this.gameObject = gameObject;
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
			return gameObject.transform.position;
		}

		public GameObject Get_GameObject()
		{
			return gameObject;
		}

		public void Set_Range_Values(ref IRange range_Values)
		{
			range_Values = this.range_Values;
		}

		public string To_String()
		{
			return gameObject.transform.name + Get_Position();
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