namespace Kadoy.ZooWorld
{
    public interface IMovementDataVisitor<out TResult>
    {
        TResult Visit(LinearMovementData data);
        TResult Visit(JumpMovementData data);
    }
}
