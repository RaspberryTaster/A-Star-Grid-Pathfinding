namespace Raspberry.Movement
{
    public class Range_Null : IRange
    {
        public Range_Values Range_Values;

        public Range_Null()
        {
            Range_Values = new Range_Values(attackRange: 1, gameplayRadius: 1, interactionRange: 1);
        }

        public Range_Null(Range_Values range_Interface_Values)
        {
            Range_Values = range_Interface_Values;
        }

        public float Get_Attack_Radius()
        {
            return Range_Values.Attack_Radius;
        }

        public float Get_Gameplay_Radius()
        {
            return Range_Values.Gameplay_Radius;
        }

        public float Get_InteractionRadius()
        {
            return Range_Values.Interaction_Radius;
        }

        public float Get_Range_Value(float range)
        {
            return range + Get_Gameplay_Radius();
        }
    }
}