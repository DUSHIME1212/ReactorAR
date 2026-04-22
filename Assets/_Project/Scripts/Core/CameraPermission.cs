using System;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Reactor.Core
{
    public class CameraPermission : MonoBehaviour
    {
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
                callbacks.PermissionDeniedAndDontAskAgain += (s) => onResult?.Invoke(false);
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
