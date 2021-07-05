using Raspberry.Movement;
using Raspberry.Movement.Actions;

public class MoveAction : IAction
{
	private Unit unit;
	private StateMachine queueComponent;
	private Node selectedNode;
	private int stoppingDistance;
	public IAction[] preActions;
	private bool reachedDestination = false;

	public MoveAction(Unit unit, StateMachine queueComponent, Node selectedNode, int stoppingDistance, IAction[] preActions = null)
	{
		this.unit = unit;
		this.queueComponent = queueComponent;
		this.selectedNode = selectedNode;
		this.stoppingDistance = stoppingDistance;
		this.preActions = preActions;
		this.preActions = preActions ?? (new IAction[0]);
		reachedDestination = false;
	}

	public void Start()
	{
		bool notReady = false;
		for (int i = 0; i < preActions.Length; i++)
		{
			if (!preActions[i].IsDone())
			{
				queueComponent.Delay_Action_With_Action(this, preActions[i], Action_Types.DELAY_WITH_MOVE);
				notReady = true;
				break;
			}
		}
		if (notReady) return;
		unit.Move(selectedNode.worldPosition, stoppingDistance, ReachedDestination);
	}

	public void ReachedDestination(bool reached)
	{
		reachedDestination = reached;
	}
	public void Update()
	{
		if (reachedDestination)
		{
			Exit();
		}
	}

	public void Exit()
	{
		queueComponent.Dequeue(this);
		unit.SetDistanceNodes();
	}

	public bool IsDone()
	{
		return reachedDestination;
	}
}
