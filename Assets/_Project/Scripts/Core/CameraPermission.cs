using System;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Reactor.Core
{
    public class CameraPermission : MonoBehaviour
    {
        private static CameraPermission _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            // Must use root GO since this is a child of [GLOBAL_SYSTEMS]
            DontDestroyOnLoad(transform.root.gameObject);
            ServiceLocator.Register<CameraPermission>(this);
        }

        private void Start()
        {
            RequestPermission(result => 
            {
                if (result)
                    Debug.Log("<color=green>CameraPermission: Permission granted.</color>");
                else
                    Debug.LogWarning("<color=red>CameraPermission: Permission denied.</color>");
            });
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                ServiceLocator.Unregister<CameraPermission>();
            }
        }

        public void RequestPermission(Action<bool> onResult)
        {
#if UNITY_ANDROID
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                onResult?.Invoke(true);
            }
            else
            {
                var callbacks = new PermissionCallbacks();
                callbacks.PermissionGranted += (s) => onResult?.Invoke(true);
                callbacks.PermissionDenied += (s) => onResult?.Invoke(false);
                Permission.RequestUserPermission(Permission.Camera, callbacks);
            }
#elif UNITY_IOS
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
            StartCoroutine(CheckIOSPermission(onResult));
#else
            onResult?.Invoke(true);
#endif
        }

#if UNITY_IOS
        private System.Collections.IEnumerator CheckIOSPermission(Action<bool> onResult)
        {
            yield return new WaitForSeconds(0.1f);
            onResult?.Invoke(Application.HasUserAuthorization(UserAuthorization.WebCam));
        }
#endif
    }
}
