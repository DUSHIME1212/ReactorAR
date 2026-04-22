using UnityEngine;

namespace Reactor.AR
{
    public class PlacementIndicator : MonoBehaviour
    {
        public GameObject visual;
        private ARRaycastHandler _raycastHandler;

        private void Awake()
        {
            _raycastHandler = FindAnyObjectByType<ARRaycastHandler>();
            if (visual != null) visual.SetActive(false);
        }

        private void Update()
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            
            if (_raycastHandler.PerformRaycast(screenCenter, out Pose pose))
            {
                transform.position = pose.position;
                transform.rotation = pose.rotation;
                
                if (visual != null && !visual.activeSelf) 
                    visual.SetActive(true);
            }
            else
            {
                if (visual != null && visual.activeSelf) 
                    visual.SetActive(false);
            }
        }
    }
}
