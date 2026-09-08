using UnityEngine;

namespace Kadoy.ZooWorld
{
    public interface IMovementStrategy
    {
        MovementStep Step(float deltaTime, Vector2 direction);
    }

    public readonly struct MovementStep
    {
        public Vector2 Displacement { get; }
        public Vector2 Direction { get; }
        public float Height { get; }

        public MovementStep(Vector2 displacement, Vector2 direction, float height = 0f)
        {
            Displacement = displacement;
            Direction = direction;
            Height = height;
        }
    }
}
