using UnityEngine;

namespace Raspberry.Movement
{
    public class Targetable_Component : MonoBehaviour, ITargetable
    {
        ITargetable current_Strategy;
        private void Start()
        {
            current_Strategy = new Targetable(gameObject);
        }
        public void Targeted_For_Destination(Movement_Input movement_Input, IRange range, ITraversal_Method traversal_Method)
        {
            current_Strategy.Targeted_For_Destination(movement_Input, range, traversal_Method);
        }

    }
}
