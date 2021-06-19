namespace Raspberry.Movement
{
    public struct Range_Values
    {
        public float Attack_Radius;
        public float Gameplay_Radius;
        public float Interaction_Radius;

        public Range_Values(float attackRange, float gameplayRadius, float interactionRange)
        {
            Attack_Radius = attackRange;
            Gameplay_Radius = gameplayRadius;
            Interaction_Radius = interactionRange;
        }
    }
}