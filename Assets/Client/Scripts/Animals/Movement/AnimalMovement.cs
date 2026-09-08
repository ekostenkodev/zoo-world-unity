using UnityEngine;
using Random = UnityEngine.Random;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalMovement
    {
        private const float MinDirectionDurationSeconds = 1.5f;
        private const float MaxDirectionDurationSeconds = 3.5f;
        private const float CollisionRecoveryDurationSeconds = 0.2f;

        private readonly AnimalPopulation _population;
        private readonly Camera _camera;
        private readonly AnimalMovementPresenter _presenter;

        public AnimalMovement(AnimalPopulation population, Camera camera, AnimalMovementPresenter presenter)
        {
            _population = population;
            _camera = camera;
            _presenter = presenter;
        }

        public void Tick(float deltaTime)
        {
            var bounds = WorldBounds.FromCamera(_camera);
            foreach (var animal in _population.Animals)
            {
                UpdateDirection(animal, bounds, deltaTime);
                var step = animal.Movement.Step(deltaTime, animal.MovementState.Direction);
                UpdateVelocity(animal, step, deltaTime);
                _presenter.Apply(animal, step);
            }
        }

        public void AllowBounce(Animal animal)
        {
            animal.MovementState.CollisionRecoveryRemaining = CollisionRecoveryDurationSeconds;
        }

        private static void UpdateDirection(Animal animal, WorldBounds bounds, float deltaTime)
        {
            var state = animal.MovementState;
            state.DirectionTimeRemaining -= deltaTime;

            var position = animal.Position;
            if (false == bounds.Contains(position))
            {
                state.Direction = bounds.DirectionToCenter(position);
                return;
            }

            if (state.DirectionTimeRemaining <= 0f)
            {
                ChooseRandomDirection(state);
            }
        }

        private static void ChooseRandomDirection(AnimalMovementState state)
        {
            var angle = Random.Range(0f, Mathf.PI * 2f);
            state.Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            state.DirectionTimeRemaining = Random.Range(MinDirectionDurationSeconds, MaxDirectionDurationSeconds);
        }

        private static void UpdateVelocity(Animal animal, MovementStep step, float deltaTime)
        {
            var state = animal.MovementState;
            if (state.CollisionRecoveryRemaining > 0f)
            {
                state.CollisionRecoveryRemaining -= deltaTime;
                return;
            }

            var velocity = step.Displacement / deltaTime;
            animal.Body.linearVelocity = new Vector3(velocity.x, 0f, velocity.y);
        }
    }
}
