using System;
using System.Linq;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalLifecycle : IDisposable
    {
        private readonly AnimalFactory _factory;
        private readonly AnimalPopulation _population;

        public AnimalLifecycle(AnimalFactory factory, AnimalPopulation population)
        {
            _factory = factory;
            _population = population;
        }

        public Animal Spawn(AnimalData data, Vector2 position)
        {
            var animal = _factory.Create(data, position);
            _population.Add(animal);
            return animal;
        }

        public bool Despawn(Animal animal)
        {
            if (false == _population.Remove(animal))
            {
                return false;
            }

            _factory.Release(animal);
            return true;
        }

        public void Dispose()
        {
            foreach (var animal in _population.Animals.ToArray())
            {
                Despawn(animal);
            }

            _factory.Clear();
        }
    }
}
