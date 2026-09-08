using R3;
using R3.Triggers;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalCollisionHandler
    {
        private readonly AnimalPopulation _population;
        private readonly AnimalMovement _movement;
        private readonly IAnimalFeeding _feeding;

        public AnimalCollisionHandler(AnimalPopulation population, AnimalMovement movement, IAnimalFeeding feeding)
        {
            _population = population;
            _movement = movement;
            _feeding = feeding;
        }

        public void Observe(Animal animal)
        {
            animal.View.OnCollisionEnterAsObservable()
                .TakeUntil(animal.View.OnDisableAsObservable())
                .Subscribe(collision =>
                {
                    HandleCollision(animal, collision);
                });
        }

        private void HandleCollision(Animal first, Collision collision)
        {
            if (false == _population.Contains(first) || collision.rigidbody == null ||
                false == _population.TryGet(collision.rigidbody, out var second) || first == second)
            {
                return;
            }

            _movement.AllowBounce(first);
            _movement.AllowBounce(second);
            _feeding.TryFeed(first, second);
        }
    }
}
