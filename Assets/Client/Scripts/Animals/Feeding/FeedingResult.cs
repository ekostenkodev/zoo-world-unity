namespace Kadoy.ZooWorld
{
    public readonly struct FeedingResult
    {
        public Animal Predator { get; }
        public Animal Victim { get; }

        public FeedingResult(Animal predator, Animal victim)
        {
            Predator = predator;
            Victim = victim;
        }
    }
}
