using System;

namespace Kadoy.ZooWorld
{
    [Serializable]
    [AddTypeMenu("Predator")]
    public sealed class PredatorRole : FoodChainRole
    {
        public override bool IsPredator => true;

        public override bool CanEat(AnimalData target)
        {
            return true;
        }
    }
}
