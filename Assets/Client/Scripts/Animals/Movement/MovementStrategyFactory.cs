namespace Kadoy.ZooWorld
{
    public sealed class MovementStrategyFactory : IMovementDataVisitor<IMovementStrategy>
    {
        public IMovementStrategy Create(MovementData data)
        {
            return data.Accept(this);
        }

        public IMovementStrategy Visit(LinearMovementData data)
        {
            return new LinearMovementStrategy(data.SpeedMetersPerSecond);
        }

        public IMovementStrategy Visit(JumpMovementData data)
        {
            return new JumpMovementStrategy(data.DistanceMeters, data.DurationSeconds, data.IntervalSeconds, data.HeightMeters);
        }
    }
}
