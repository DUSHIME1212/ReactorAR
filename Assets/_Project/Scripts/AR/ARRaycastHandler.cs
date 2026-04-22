using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Reactor.AR
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARRaycastHandler : MonoBehaviour
    {
        private ARRaycastManager _raycastManager;
        private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        public event System.Action<Pose> OnHitPose;

        private void Awake()
        {
            _raycastManager = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;

            if (_raycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = s_Hits[0].pose;
                OnHitPose?.Invoke(hitPose);
            }
        }

        public bool PerformRaycast(Vector2 screenPos, out Pose pose)
        {
            if (_raycastManager.Raycast(screenPos, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                pose = s_Hits[0].pose;
                return true;
            }
            pose = default;
            return false;
        }
    }
}
