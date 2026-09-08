using System;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    [Serializable]
    [AddTypeMenu("Linear")]
    public sealed class LinearMovementData : MovementData
    {
        [SerializeField, Min(0.01f)] private float _speedMetersPerSecond = 1.8f;

        public float SpeedMetersPerSecond => _speedMetersPerSecond;

        public override TResult Accept<TResult>(IMovementDataVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
