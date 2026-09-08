using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalMovementPresenter
    {
        public void Apply(Animal animal, MovementStep step)
        {
            var visual = animal.View.VisualRoot;
            visual.localPosition = Vector3.up * step.Height;
            visual.localRotation = Quaternion.LookRotation(new Vector3(step.Direction.x, 0f, step.Direction.y));
        }
    }
}
