namespace Kadoy.ZooWorld
{
    public static class FoodChainRules
    {
        public static bool TryGetVictim(Animal first, Animal second, out int victimId)
        {
            victimId = 0;
            if (first.Id == second.Id)
            {
                return false;
            }

            var firstCanEat = first.Data.Role.CanEat(second.Data);
            var secondCanEat = second.Data.Role.CanEat(first.Data);
            if (false == firstCanEat && false == secondCanEat)
            {
                return false;
            }

            if (firstCanEat && secondCanEat)
            {
                victimId = System.Math.Max(first.Id, second.Id);
            }
            else
            {
                victimId = firstCanEat ? second.Id : first.Id;
            }

            return true;
        }
    }
}
