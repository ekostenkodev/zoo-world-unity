using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalMovementState
    {
        public Vector2 Direction { get; set; } = Vector2.up;
        public float DirectionTimeRemaining { get; set; }
        public float CollisionRecoveryRemaining { get; set; }
    }
}
