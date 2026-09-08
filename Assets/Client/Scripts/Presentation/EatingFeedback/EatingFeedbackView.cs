using UnityEngine;

namespace Kadoy.ZooWorld
{
    public sealed class EatingFeedbackView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _facingRoot;

        public CanvasGroup CanvasGroup => _canvasGroup;
        public Transform FacingRoot => _facingRoot;
    }
}
