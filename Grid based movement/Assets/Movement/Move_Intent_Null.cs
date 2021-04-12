using UnityEngine;

namespace Raspberry.Movement
{
	public class Move_Intent_Null : IMove_Intent
	{
		public float range;

		public Move_Intent_Null(float range)
		{
			this.range = range;
		}

		public float Effective_Range()
		{
			return range;
		}

		public void Execute()
		{
			Debug.Log("I just wanted to get closer :)");
		}
	}
}
