using System;

namespace Kadoy.ZooWorld
{
    [Serializable]
    public abstract class MovementData
    {
        public abstract TResult Accept<TResult>(IMovementDataVisitor<TResult> visitor);
    }
}
