namespace Raspberry.Movement
{
    public interface IRange
    {
        float Get_Attack_Radius();
        float Get_Gameplay_Radius();
        float Get_InteractionRadius();
        float Get_Range_Value(float range);
    }
}
