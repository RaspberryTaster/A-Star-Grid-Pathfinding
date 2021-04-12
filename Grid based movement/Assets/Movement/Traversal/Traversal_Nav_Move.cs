using UnityEngine;
using UnityEngine.AI;

namespace Raspberry.Movement
{
	public class Traversal_Nav_Move : ITraversal_Method
	{
		private NavMeshAgent navMeshAgent;

		public Traversal_Nav_Move(NavMeshAgent navMeshAgent)
		{
			this.navMeshAgent = navMeshAgent;
		}

		public void Set_Destination(Vector3 position)
		{
			navMeshAgent.SetDestination(position);
		}
	}
}
