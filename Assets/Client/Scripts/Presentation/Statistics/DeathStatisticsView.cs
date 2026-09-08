using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.ZooWorld
{
    public sealed class DeathStatisticsView : MonoBehaviour
    {
        [SerializeField] private Text _deadPreyText;
        [SerializeField] private Text _deadPredatorsText;

        public Text DeadPreyText => _deadPreyText;
        public Text DeadPredatorsText => _deadPredatorsText;
    }
}
