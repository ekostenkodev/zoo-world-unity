namespace Kadoy.ZooWorld
{
    public sealed class DeathStatistics
    {
        private DeathCounts _counts;

        public DeathCounts Current => _counts;

        public void RecordDeath(FoodChainRole role)
        {
            if (role.IsPredator)
            {
                _counts = new DeathCounts(_counts.Prey, _counts.Predators + 1);
            }
            else
            {
                _counts = new DeathCounts(_counts.Prey + 1, _counts.Predators);
            }
        }
    }
}
