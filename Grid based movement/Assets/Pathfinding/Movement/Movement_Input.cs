using Raspberry.Movement.Actions;
using Raspberry.Movement.Target;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Raspberry.Movement
{
    public class Movement_Input : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        public Vector3 cur_Mouse_Position;
        public StateMachine queue_Component;
        public Movement_Component movement_Component;
        public List<ITraversal_Method> traversal_Methods = new List<ITraversal_Method>();
        public int traversal_Index;
        private int fingerID = -1;
        void Start()
        {
            cam = Camera.main;
            Update_Unit();
        }

        private void Update_Unit()
        {
            if (movement_Component == null)
            {
                traversal_Methods.Clear();
            }
            else
            {
                ITraversal_Method teleport = new Traversal_Teleport(movement_Component.gameObject);
                ITraversal_Method walk = new Traversal_Nav_Move(movement_Component.NavMeshAgent);
                traversal_Methods = new List<ITraversal_Method> { teleport, walk };
            }
        }

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject(fingerID)) return;
            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 900))
                {
                    cur_Mouse_Position = hit.point;


                    ITargetable Targetable = hit.transform.gameObject.GetComponent<Targetable_Component>();
                    if (Targetable == null)
                    {
                        Targetable = new Targetable_Null();
                    }

                    IRange Range = hit.transform.gameObject.GetComponent<Range_Component>();
                    if (Range == null)
                    {
                        Range = new Range_Null();
                    }
                    if (traversal_Index > traversal_Methods.Count - 1) return;
                    Targetable.Targeted_For_Destination(this, Range, traversal_Methods[traversal_Index]);

                }
            }
           
            if (Input.GetKeyDown(KeyCode.S))
            {
                movement_Component.Movement_Handler.Idle();
            }
        }
        public void SetEnviromentDestination(Vector3 position, ITraversal_Method traversal_Method)
        {
            Range_Values range_Interface_Values = new Range_Values(0.1f, 0.1f, 0.1f);
            IRange range_Temp = new Range_Null(range_Interface_Values);
            Target_Position target = new Target_Position(position, range_Temp);
            IMove_Intent move_Intent = new Move_Intent_Null(0.6f);
            movement_Component.Move(target, traversal_Method, move_Intent, Action_Types.ENVIROMENT_MOVE);
        }

        public void SetObjectDestination(GameObject gameObject, IRange range, ITraversal_Method traversal_Method)
        {
            //action_Component.Execute(gameObject, range, traversal_Method);
        }
    }
}
