using System;

namespace Kadoy.ZooWorld
{
    [Serializable]
    [AddTypeMenu("Prey")]
    public sealed class PreyRole : FoodChainRole
    {
        public override bool IsPredator => false;

        public override bool CanEat(AnimalData target)
        {
            return false;
        }
    }
}
