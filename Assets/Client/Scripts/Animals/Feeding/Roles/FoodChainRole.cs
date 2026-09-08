using System;

namespace Kadoy.ZooWorld
{
    [Serializable]
    public abstract class FoodChainRole
    {
        public abstract bool IsPredator { get; }

        public abstract bool CanEat(AnimalData target);
    }
}
