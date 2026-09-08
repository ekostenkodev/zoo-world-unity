using R3;

namespace Kadoy.ZooWorld
{
    public interface IAnimalFeeding
    {
        Observable<FeedingResult> Fed { get; }

        bool TryFeed(Animal first, Animal second);
    }
}
