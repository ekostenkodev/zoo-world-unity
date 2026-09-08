using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class AnimalView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _collisionShape;
        [SerializeField] private Transform _visualRoot;

        public Rigidbody Rigidbody => _rigidbody;
        public SphereCollider CollisionShape => _collisionShape;
        public Transform VisualRoot => _visualRoot;
    }
}
