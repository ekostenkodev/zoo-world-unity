namespace Kadoy.ZooWorld
{
    public readonly struct DeathCounts
    {
        public int Prey { get; }
        public int Predators { get; }

        public DeathCounts(int prey, int predators)
        {
            Prey = prey;
            Predators = predators;
        }
    }
}
