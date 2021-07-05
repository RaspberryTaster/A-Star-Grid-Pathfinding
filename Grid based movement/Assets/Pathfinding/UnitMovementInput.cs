using Assets;
using Raspberry.Movement;
using Raspberry.Movement.Actions;
using UnityEngine;

public class UnitMovementInput : MonoBehaviour
{
	public DistanceCheck SurroundingNodes;
	public Node selectedNode;
	public SquareGrid squareGrid;
	public Unit unit;
	public CombatComponent controlledUnitCombatComponent;
	public StateMachine stateMachine;
	PlayerInput playerInput;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controlledUnitCombatComponent = unit.GetComponent<CombatComponent>();
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
		unit.SetDistanceNodes();

		Unit targetUnit = hit.collider.GetComponent<Unit>();
		Node targetUnitNode = null;
		if (targetUnit != null)
		{
			targetUnitNode = squareGrid.NodeGrid.NodeFromWorldPoint(targetUnit.transform.position);
		}
		Node hitNode = squareGrid.NodeGrid.NodeFromWorldPoint(hit.point);
		CombatComponent targtCombatComponent = hit.collider.GetComponent<CombatComponent>();

		int stoppingDistance = 0;
		IAction action;
		Action_Types action_Types;
		if (targetUnit != null && targetUnitNode != null && unit.WithinRangeNodes.Contains(targetUnitNode))
		{
			SurroundingNodes.SetUp(unit.transform, targetUnitNode, controlledUnitCombatComponent.minAttackRange, controlledUnitCombatComponent.maxAttackRange);
			selectedNode = SurroundingNodes.closestNode;
			MoveAction moveAction = new MoveAction(unit, stateMachine, selectedNode, 0);
			action = new AttackAction(controlledUnitCombatComponent, stateMachine, targtCombatComponent, moveAction);
			action_Types = Action_Types.ATTACK;
		}
		else
		{
			selectedNode = hitNode;
			action = new MoveAction(unit, stateMachine, selectedNode, stoppingDistance);
			action_Types = Action_Types.WALK;
		}
		if (selectedNode == null || !selectedNode.walkable || action == null || !unit.MovementNodes.Contains(selectedNode)) return;
		stateMachine.Dequeue_All_Before_Adding_Action(action, action_Types);
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
