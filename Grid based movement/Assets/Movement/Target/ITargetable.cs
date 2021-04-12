namespace Raspberry.Movement
{
	public interface ITargetable
	{
		void Targeted_For_Destination(Movement_Input playerInput, IRange range, ITraversal_Method traversal_Method);
	}
}