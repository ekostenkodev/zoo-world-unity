using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class EatingFeedbackPresenter : IDisposable
    {
        private const float FeedbackDurationSeconds = 1f;

        private readonly IAnimalFeeding _feeding;
        private readonly EatingFeedbackView _prefab;
        private readonly Camera _camera;
        private readonly Dictionary<Animal, IDisposable> _visibleFeedback = new();
        private readonly CompositeDisposable _subscriptions = new();

        public EatingFeedbackPresenter(IAnimalFeeding feeding, EatingFeedbackView prefab, Camera camera)
        {
            _feeding = feeding;
            _prefab = prefab;
            _camera = camera;
        }

        public void Initialize()
        {
            _subscriptions.Clear();
            _feeding.Fed.Subscribe(OnFed).AddTo(_subscriptions);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();

            foreach (var feedback in _visibleFeedback.Values)
            {
                feedback.Dispose();
            }

            _visibleFeedback.Clear();
        }

        private void OnFed(FeedingResult result)
        {
            Hide(result.Victim);
            Show(result.Predator);
        }

        private void Show(Animal animal)
        {
            Hide(animal);

            var view = CreateView(animal);
            var timer = ScheduleHide(animal);
            var cleanup = Disposable.Create(() =>
            {
                DestroyView(view);
            });

            _visibleFeedback.Add(animal, Disposable.Combine(timer, cleanup));
        }

        private void Hide(Animal animal)
        {
            if (_visibleFeedback.Remove(animal, out var feedback))
            {
                feedback.Dispose();
            }
        }

        private EatingFeedbackView CreateView(Animal animal)
        {
            var view = UnityEngine.Object.Instantiate(_prefab, animal.View.transform);
            view.FacingRoot.rotation = _camera.transform.rotation;
            view.CanvasGroup.alpha = 1f;
            return view;
        }

        private IDisposable ScheduleHide(Animal animal)
        {
            return Observable.Timer(TimeSpan.FromSeconds(FeedbackDurationSeconds), UnityTimeProvider.Update)
                .Subscribe(_ =>
                {
                    Hide(animal);
                });
        }

        private static void DestroyView(EatingFeedbackView view)
        {
            if (view == null)
            {
                return;
            }

            view.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(view.gameObject);
        }
    }
}
