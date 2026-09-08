using System;
using R3;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalFeeding : IAnimalFeeding, IDisposable
    {
        private readonly AnimalPopulation _population;
        private readonly AnimalLifecycle _lifecycle;
        private readonly Subject<FeedingResult> _fed = new();

        public Observable<FeedingResult> Fed => _fed;

        public AnimalFeeding(AnimalPopulation population, AnimalLifecycle lifecycle)
        {
            _population = population;
            _lifecycle = lifecycle;
        }

        public bool TryFeed(Animal first, Animal second)
        {
            if (false == _population.Contains(first) || false == _population.Contains(second) ||
                false == FoodChainRules.TryGetVictim(first, second, out var victimId))
            {
                return false;
            }

            var victim = victimId == first.Id ? first : second;
            var predator = victimId == first.Id ? second : first;

            if (false == _lifecycle.Despawn(victim))
            {
                return false;
            }

            _fed.OnNext(new FeedingResult(predator, victim));

            return true;
        }

        public void Dispose()
        {
            _fed.Dispose();
        }
    }
}
