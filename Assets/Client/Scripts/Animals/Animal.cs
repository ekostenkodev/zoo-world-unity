using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class Animal
    {
        public int Id { get; }
        public AnimalData Data { get; }
        public AnimalView View { get; }
        public Rigidbody Body { get; }
        public IMovementStrategy Movement { get; }
        public AnimalMovementState MovementState { get; } = new();

        public float Radius => View.CollisionShape.radius;
        public Vector2 Position => new(Body.position.x, Body.position.z);

        public Animal(int id, AnimalData data, AnimalView view, IMovementStrategy movement)
        {
            Id = id;
            Data = data;
            View = view;
            Body = view.Rigidbody;
            Movement = movement;
        }
    }
}
