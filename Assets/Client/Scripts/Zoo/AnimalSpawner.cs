using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalSpawner
    {
        private readonly ZooData _data;
        private readonly AnimalLifecycle _lifecycle;
        private readonly AnimalCollisionHandler _collisions;
        private readonly Camera _camera;
        private float _timeRemaining;

        public AnimalSpawner(ZooData data, AnimalLifecycle lifecycle, AnimalCollisionHandler collisions,
            Camera camera)
        {
            _data = data;
            _lifecycle = lifecycle;
            _collisions = collisions;
            _camera = camera;
        }

        public void Initialize()
        {
            _timeRemaining = Random.Range(1f, 2f);
        }

        public void Tick(float deltaTime)
        {
            _timeRemaining -= deltaTime;
            if (_timeRemaining > 0f)
            {
                return;
            }

            _timeRemaining = Random.Range(1f, 2f);
            var data = _data.Animals[Random.Range(0, _data.Animals.Count)];
            var radius = data.Prefab.CollisionShape.radius;
            var bounds = WorldBounds.FromCamera(_camera);
            var position = bounds.GetPoint(Random.value, Random.value, radius + 0.5f);
            var animal = _lifecycle.Spawn(data, position);
            _collisions.Observe(animal);
        }
    }
}
