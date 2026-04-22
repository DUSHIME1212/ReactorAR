using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Reactor.AR
{
    public class ARSessionManager : MonoBehaviour
    {
        private ARSession _arSession;

        private void Awake()
        {
            _arSession = FindAnyObjectByType<ARSession>();
        }

        public void ResetSession()
        {
            if (_arSession != null)
            {
                _arSession.Reset();
                Debug.Log("AR Session Reset");
            }
        }

        public void SetSessionActive(bool active)
        {
            if (_arSession != null)
            {
                _arSession.enabled = active;
            }
        }
    }
}
