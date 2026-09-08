using System;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    [Serializable]
    [AddTypeMenu("Jump")]
    public sealed class JumpMovementData : MovementData
    {
        [SerializeField, Min(0.01f)] private float _distanceMeters = 1.7f;
        [SerializeField, Min(0.01f)] private float _durationSeconds = 0.45f;
        [SerializeField, Min(0.01f)] private float _intervalSeconds = 1.35f;
        [SerializeField, Min(0f)] private float _heightMeters = 0.65f;

        public float DistanceMeters => _distanceMeters;
        public float DurationSeconds => _durationSeconds;
        public float IntervalSeconds => _intervalSeconds;
        public float HeightMeters => _heightMeters;

        public override TResult Accept<TResult>(IMovementDataVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
