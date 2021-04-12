using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Raspberry.Movement
{
	public class Distance_Limiter : MonoBehaviour
	{
		public int Movement_Points;
		[SerializeField] private float distance; //variable for total distance
		private Vector3 oldPos = new Vector3(0.0f, 0.0f, 0.0f);
		public bool Reached_Max_Distance { get => Movement_Points <= distance; }
		private void FixedUpdate()
		{
			oldPos = transform.position;
		}
		private void LateUpdate()
		{
			distance += Vector3.Distance(oldPos, transform.position);
		}
	}

}
