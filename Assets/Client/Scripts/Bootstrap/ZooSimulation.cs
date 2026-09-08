using UnityEngine;
using VContainer.Unity;

namespace Kadoy.ZooWorld
{
    public sealed class ZooSimulation : IStartable, ITickable, IFixedTickable
    {
        private readonly AnimalSpawner _spawner;
        private readonly AnimalMovement _movement;
        private readonly DeathStatisticsPresenter _statisticsPresenter;
        private readonly EatingFeedbackPresenter _feedbackPresenter;

        public ZooSimulation(AnimalSpawner spawner, AnimalMovement movement,
            DeathStatisticsPresenter statisticsPresenter, EatingFeedbackPresenter feedbackPresenter)
        {
            _spawner = spawner;
            _movement = movement;
            _statisticsPresenter = statisticsPresenter;
            _feedbackPresenter = feedbackPresenter;
        }

        public void Start()
        {
            _statisticsPresenter.Initialize();
            _feedbackPresenter.Initialize();
            _spawner.Initialize();
        }

        public void Tick()
        {
            _spawner.Tick(Time.deltaTime);
        }

        public void FixedTick()
        {
            _movement.Tick(Time.fixedDeltaTime);
        }
    }
}
