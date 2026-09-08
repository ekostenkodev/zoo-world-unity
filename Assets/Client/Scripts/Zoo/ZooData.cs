using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kadoy.ZooWorld
{
    [CreateAssetMenu(menuName = "Zoo World/Zoo")]
    public sealed class ZooData : ScriptableObject
    {
        [SerializeField] private AnimalData[] _animals = Array.Empty<AnimalData>();

        public IReadOnlyList<AnimalData> Animals => _animals;
    }
}
