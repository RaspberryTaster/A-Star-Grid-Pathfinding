using Raspberry.Movement;
using Raspberry.Movement.Actions;
using UnityEngine;

public class TalkAction : IAction
{
	private Unit unit;
	private Unit targetUnit;
	private StateMachine queueComponent;
	public IAction[] preActions = new IAction[0];
	bool hasTalked = false;

	public TalkAction(Unit unit, Unit targetUnit, StateMachine queueComponent, params IAction[] preActions)
	{
		this.unit = unit;
		this.targetUnit = targetUnit;
		this.queueComponent = queueComponent;
		this.preActions = preActions;
	}

	public bool IsDone()
	{
		return hasTalked;
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
		Debug.Log($"{unit.gameObject.name}: Hey, {targetUnit.gameObject.name}? how will you defend yourself?");
		hasTalked = true;
	}

	public void Update()
	{
		if (IsDone())
		{
			Exit();
		}
	}
	public void Exit()
	{
		queueComponent.Dequeue(this);
	}

}
