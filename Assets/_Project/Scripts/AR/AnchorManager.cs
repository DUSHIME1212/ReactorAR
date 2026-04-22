using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Reactor.AR
{
    [RequireComponent(typeof(ARAnchorManager))]
    public class AnchorManager : MonoBehaviour
    {
        private ARAnchorManager _anchorManager;
        private ARAnchor _currentAnchor;

        private void Awake()
        {
            _anchorManager = GetComponent<ARAnchorManager>();
        }

        public ARAnchor CreateAnchor(Pose pose)
        {
            if (_currentAnchor != null)
            {
                Destroy(_currentAnchor.gameObject);
            }

            // Universal way to create an anchor in ARFoundation:
            // Create a GameObject and add the ARAnchor component.
            // The ARAnchorManager will automatically discover and manage it.
            var anchorGO = new GameObject("ARAnchor");
            anchorGO.transform.position = pose.position;
            anchorGO.transform.rotation = pose.rotation;
            _currentAnchor = anchorGO.AddComponent<ARAnchor>();
            
            return _currentAnchor;
        }

        public void ClearAnchor()
        {
            if (_currentAnchor != null)
            {
                Destroy(_currentAnchor.gameObject);
                _currentAnchor = null;
            }
        }
    }
}
