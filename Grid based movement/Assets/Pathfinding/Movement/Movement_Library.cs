using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Raspberry.Library.Movement
{
    public static class Movement_Library
    {
        public static bool NavMesh_Stopped(NavMeshAgent agent)
        {
            if (agent.velocity.magnitude == 0) return true;
            return false;
        }

        public static bool NavMesh_At_Destination(NavMeshAgent agent)
        {
            bool Value = false;
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
            {
                Value = true;
            }
            return Value;
        }

        public static void NavMesh_Stop(NavMeshAgent agent)
        {
            agent.stoppingDistance = 0;
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}

