using UnityEngine;

namespace Library.Rotation
{
	public static class Rotation_Library
	{
		public static void Face_Target(Transform self_Transform, Vector3 targetPosition)
		{
			Vector3 direction = (targetPosition - self_Transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			self_Transform.rotation = lookRotation;
		}

		public static bool Is_Facing_Target(Transform self_Transform, Vector3 target_Position)
		{
			Vector3 direction = (target_Position - self_Transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			return (self_Transform.rotation == lookRotation);
		}
	}
}