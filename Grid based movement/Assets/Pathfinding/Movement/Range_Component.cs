using UnityEngine;

namespace Raspberry.Movement
{
	public class Range_Component : MonoBehaviour, IRange
	{
		[SerializeField] private float attack_Range = 1f;

		[SerializeField] private float interaction_Range = 1f;

		[SerializeField] private float gameplay_Radius = 1f;

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, Get_Attack_Radius());
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, Get_InteractionRadius());
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position, Get_Gameplay_Radius());
		}

		public float Get_Attack_Radius()
		{
			return attack_Range + Get_Gameplay_Radius();

		}

		public float Get_Gameplay_Radius()
		{
			return gameplay_Radius + Get_Largest_Scale();

		}

		private float Get_Largest_Scale()
		{
			Vector3 localScale = gameObject.transform.localScale;

			if (localScale.x > localScale.y)
			{
				if (localScale.x > localScale.z)
				{
					return localScale.x;
				}
				else
				{
					return localScale.z;
				}
			}
			else
			{
				if (localScale.y > localScale.z)
				{
					return localScale.y;
				}
				else
				{
					return localScale.z;
				}
			}
		}

		public float Get_InteractionRadius()
		{
			return interaction_Range + Get_Gameplay_Radius();

		}

		public float Get_Range_Value(float range)
		{
			return range + Get_Gameplay_Radius();
		}
	}
}
