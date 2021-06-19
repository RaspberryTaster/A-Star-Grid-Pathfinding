namespace Raspberry.Movement
{
	public class Targetable_Null : ITargetable
	{

		public void Targeted_For_Destination(Movement_Input movement_Input, IRange range, ITraversal_Method traversal_Method)
		{
			movement_Input.SetEnviromentDestination(movement_Input.cur_Mouse_Position, traversal_Method);
		}
	}
}
