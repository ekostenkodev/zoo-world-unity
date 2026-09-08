using System.Collections.Generic;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalFactory
    {
        private readonly Transform _parent;
        private readonly MovementStrategyFactory _movementFactory;
        private readonly Dictionary<AnimalView, Stack<AnimalView>> _pool = new();
        private int _nextId;

        public AnimalFactory(Transform parent, MovementStrategyFactory movementFactory)
        {
            _parent = parent;
            _movementFactory = movementFactory;
        }

        public Animal Create(AnimalData data, Vector2 position)
        {
            var movement = _movementFactory.Create(data.Movement);
            var views = GetPool(data.Prefab);
            var worldPosition = new Vector3(position.x, 0f, position.y);
            var view = views.Count > 0
                ? views.Pop()
                : Object.Instantiate(data.Prefab, worldPosition, Quaternion.identity, _parent);
            view.transform.position = worldPosition;
            view.Rigidbody.position = worldPosition;
            view.Rigidbody.linearVelocity = Vector3.zero;
            view.VisualRoot.localPosition = data.Prefab.VisualRoot.localPosition;
            view.VisualRoot.localRotation = data.Prefab.VisualRoot.localRotation;
            view.gameObject.SetActive(true);
            return new Animal(++_nextId, data, view, movement);
        }

        public void Release(Animal animal)
        {
            if (animal.View == null)
            {
                return;
            }

            animal.View.gameObject.SetActive(false);
            _pool[animal.Data.Prefab].Push(animal.View);
        }

        public void Clear()
        {
            foreach (var views in _pool.Values)
            {
                foreach (var view in views)
                {
                    if (view != null)
                    {
                        Object.Destroy(view.gameObject);
                    }
                }
            }

            _pool.Clear();
        }

        private Stack<AnimalView> GetPool(AnimalView prefab)
        {
            if (false == _pool.TryGetValue(prefab, out var views))
            {
                views = new Stack<AnimalView>();
                _pool.Add(prefab, views);
            }

            return views;
        }
    }
}
