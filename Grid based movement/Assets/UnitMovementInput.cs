using Assets;
using Raspberry.Movement;
using Raspberry.Movement.Actions;
using UnityEngine;

public class UnitMovementInput : MonoBehaviour
{
	public SurroundingNodes2 SurroundingNodes;
	public Node selectedNode;
	public NodeGrid grid;
	public Unit controlledUnit;
	public CombatComponent controlledUnitCombatComponent;
	public QueueComponent queueComponent;
	public Movement_Grid_Component Movement_Grid_Component;
	PlayerInput playerInput;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controlledUnitCombatComponent = controlledUnit.GetComponent<CombatComponent>();
	}

	private void OnEnable()
	{
		playerInput.OnHitRaycast += PlayerInput_OnHitRaycast;
	}

	public void OnDisable()
	{
		playerInput.OnHitRaycast -= PlayerInput_OnHitRaycast;
	}

	private void PlayerInput_OnHitRaycast(RaycastHit hit)
	{
		//selectedNode = null;
		Movement_Grid_Component.GetDistanceNodes();

		Unit targetUnit = hit.collider.GetComponent<Unit>();
		Node targetUnitNode = null;
		if (targetUnit != null)
		{
			targetUnitNode = grid.NodeFromWorldPoint(targetUnit.transform.position);
		}
		Node hitNode = grid.NodeFromWorldPoint(hit.point);
		CombatComponent targtCombatComponent = hit.collider.GetComponent<CombatComponent>();

		int stoppingDIstance = 0;
		IAction action;
		Action_Types action_Types;
		if (targetUnit != null && targetUnitNode != null && Movement_Grid_Component.WithinRangeNodes.Contains(targetUnitNode))
		{
			SurroundingNodes.SetUp(controlledUnit.transform, targetUnitNode, controlledUnitCombatComponent.minAttackRange, controlledUnitCombatComponent.maxAttackRange);
			selectedNode = SurroundingNodes.closestNode;
			MoveAction moveAction = new MoveAction(controlledUnit, queueComponent, selectedNode, 0, Movement_Grid_Component);
			action = new AttackAction(controlledUnitCombatComponent, queueComponent, targtCombatComponent, moveAction);
			action_Types = Action_Types.ATTACK;
		}
		else
		{
			selectedNode = hitNode;
			action = new MoveAction(controlledUnit, queueComponent, selectedNode, stoppingDIstance, Movement_Grid_Component);
			action_Types = Action_Types.WALK;
		}
		if (selectedNode == null || !selectedNode.walkable || action == null || !Movement_Grid_Component.MovementNodes.Contains(selectedNode)) return;
		queueComponent.Dequeue_All_Before_Adding_Action(action, action_Types);
	}

	private void OnDrawGizmos()
	{
		if (selectedNode != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawCube(selectedNode.worldPosition, Vector3.one);
		}

	}
}
