using System;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class JumpMovementStrategy : IMovementStrategy
    {
        private readonly float _distance;
        private readonly float _duration;
        private readonly float _interval;
        private readonly float _height;
        private float _cycleTime;
        private Vector2 _jumpDirection;

        public JumpMovementStrategy(float distance, float duration, float interval, float height)
        {
            if (float.IsNaN(duration) || float.IsInfinity(duration) || duration <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Jump duration must be finite and positive.");
            }

            if (float.IsNaN(interval) || float.IsInfinity(interval) || interval < duration)
            {
                throw new ArgumentOutOfRangeException(nameof(interval), "Jump interval must be finite and at least the duration.");
            }

            _distance = distance;
            _duration = duration;
            _interval = interval;
            _height = height;
        }

        public MovementStep Step(float deltaTime, Vector2 direction)
        {
            if (float.IsNaN(deltaTime) || float.IsInfinity(deltaTime) || deltaTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time must be finite and non-negative.");
            }

            if (_cycleTime == 0f)
            {
                _jumpDirection = direction;
            }

            // Finish the current cycle using the direction captured at takeoff.
            var timeInCurrentCycle = Mathf.Min(deltaTime, _interval - _cycleTime);
            var timeInMotion = Mathf.Max(0f, Mathf.Min(_duration - _cycleTime, timeInCurrentCycle));
            var displacement = _jumpDirection * (_distance * timeInMotion / _duration);
            _cycleTime += timeInCurrentCycle;
            if (_cycleTime >= _interval)
            {
                _cycleTime = 0f;
            }

            // All subsequent cycles use the newly requested direction.
            var remainingTime = deltaTime - timeInCurrentCycle;
            if (remainingTime > 0f)
            {
                var completedCycles = Math.Floor((double)remainingTime / _interval);
                _cycleTime = remainingTime % _interval;
                _jumpDirection = direction;
                var jumpProgress = Mathf.Min(1f, _cycleTime / _duration);
                displacement += _jumpDirection * (float)(_distance * (completedCycles + jumpProgress));
            }

            var progress = Mathf.Min(1f, _cycleTime / _duration);
            return new MovementStep(displacement, _jumpDirection, _height * 4f * progress * (1f - progress));
        }
    }
}
