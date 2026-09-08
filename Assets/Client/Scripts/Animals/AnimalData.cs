using UnityEngine;

namespace Kadoy.ZooWorld
{
    [CreateAssetMenu(menuName = "Zoo World/Animal")]
    public sealed class AnimalData : ScriptableObject
    {
        [SerializeField] private AnimalView _prefab;
        [SerializeReference, SubclassSelector] private FoodChainRole _foodChainRole = new PreyRole();
        [SerializeReference, SubclassSelector] private MovementData _movement = new LinearMovementData();

        public FoodChainRole Role => _foodChainRole;
        public AnimalView Prefab => _prefab;
        public MovementData Movement => _movement;
    }
}
