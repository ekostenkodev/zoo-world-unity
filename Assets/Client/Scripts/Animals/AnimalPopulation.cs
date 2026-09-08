using System.Collections.Generic;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalPopulation
    {
        private readonly Dictionary<Rigidbody, Animal> _animals = new();

        public IReadOnlyCollection<Animal> Animals => _animals.Values;

        public void Add(Animal animal)
        {
            _animals.Add(animal.Body, animal);
        }

        public bool Contains(Animal animal)
        {
            return _animals.TryGetValue(animal.Body, out var current) && current == animal;
        }

        public bool TryGet(Rigidbody body, out Animal animal)
        {
            return _animals.TryGetValue(body, out animal);
        }

        public bool Remove(Animal animal)
        {
            return Contains(animal) && _animals.Remove(animal.Body);
        }
    }
}
