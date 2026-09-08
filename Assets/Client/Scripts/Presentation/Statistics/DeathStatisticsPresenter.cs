using System;
using R3;

namespace Kadoy.ZooWorld
{
    public sealed class DeathStatisticsPresenter : IDisposable
    {
        private readonly IAnimalFeeding _feeding;
        private readonly DeathStatistics _statistics;
        private readonly DeathStatisticsView _view;
        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        public DeathStatisticsPresenter(IAnimalFeeding feeding, DeathStatistics statistics, DeathStatisticsView view)
        {
            _feeding = feeding;
            _statistics = statistics;
            _view = view;
        }

        public void Initialize()
        {
            _subscriptions.Clear();
            _feeding.Fed.Subscribe(OnFed).AddTo(_subscriptions);
            Display(_statistics.Current);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        private void OnFed(FeedingResult result)
        {
            _statistics.RecordDeath(result.Victim.Data.Role);
            Display(_statistics.Current);
        }

        private void Display(DeathCounts counts)
        {
            _view.DeadPreyText.text = "Prey deaths: " + counts.Prey;
            _view.DeadPredatorsText.text = "Predator deaths: " + counts.Predators;
        }
    }
}
