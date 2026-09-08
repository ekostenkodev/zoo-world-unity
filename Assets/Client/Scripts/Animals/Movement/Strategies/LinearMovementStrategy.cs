using System;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class LinearMovementStrategy : IMovementStrategy
    {
        private readonly float _speed;

        public LinearMovementStrategy(float speed)
        {
            _speed = speed;
        }

        public MovementStep Step(float deltaTime, Vector2 direction)
        {
            return new MovementStep(direction * (_speed * deltaTime), direction);
        }
    }
}
