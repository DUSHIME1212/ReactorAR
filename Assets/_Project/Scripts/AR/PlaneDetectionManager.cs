using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Reactor.AR
{
    [RequireComponent(typeof(ARPlaneManager))]
    public class PlaneDetectionManager : MonoBehaviour
    {
        private ARPlaneManager _planeManager;

        public int DetectedPlaneCount => _planeManager.trackables.count;

        private void Awake()
        {
            _planeManager = GetComponent<ARPlaneManager>();
        }

        public void SetDetectionActive(bool active)
        {
            _planeManager.enabled = active;
            foreach (var plane in _planeManager.trackables)
            {
                plane.gameObject.SetActive(active);
            }
        }

        public void ClearPlanes()
        {
            // Note: ARFoundation doesn't directly allow deleting detected planes easily, 
            // but we can disable them or reset the session.
            SetDetectionActive(false);
        }
    }
}
