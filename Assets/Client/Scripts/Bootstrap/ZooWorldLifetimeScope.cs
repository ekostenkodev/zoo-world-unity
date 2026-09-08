using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Kadoy.ZooWorld
{
    public sealed class ZooWorldLifetimeScope : LifetimeScope
    {
        [SerializeField] private ZooData _zooData;
        [SerializeField] private Transform _animalRoot;
        [SerializeField] private Camera _worldCamera;
        [SerializeField] private DeathStatisticsView _statisticsView;
        [SerializeField] private EatingFeedbackView _eatingFeedbackPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_zooData);
            builder.RegisterInstance(_animalRoot);
            builder.RegisterInstance(_worldCamera);
            builder.RegisterInstance(_statisticsView);
            builder.RegisterInstance(_eatingFeedbackPrefab);
            builder.Register<MovementStrategyFactory>(Lifetime.Singleton);
            builder.Register<AnimalPopulation>(Lifetime.Singleton);
            builder.Register<AnimalFactory>(Lifetime.Singleton);
            builder.Register<AnimalLifecycle>(Lifetime.Singleton);
            builder.Register<AnimalSpawner>(Lifetime.Singleton);
            builder.Register<AnimalCollisionHandler>(Lifetime.Singleton);
            builder.Register<AnimalFeeding>(Lifetime.Singleton).As<IAnimalFeeding>();
            builder.Register<AnimalMovement>(Lifetime.Singleton);
            builder.Register<DeathStatistics>(Lifetime.Singleton);
            builder.Register<DeathStatisticsPresenter>(Lifetime.Singleton);
            builder.Register<AnimalMovementPresenter>(Lifetime.Singleton);
            builder.Register<EatingFeedbackPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ZooSimulation>().AsSelf();
        }
    }
}
