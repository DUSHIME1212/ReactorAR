using UnityEngine;
using Reactor.Core;

namespace Reactor.Core
{
    public class AppBootstrap : MonoBehaviour
    {
        [Header("Settings")]
        public int targetFrameRate = 60;
        public bool useScreenTimeout = false;

        private void Awake()
        {
            Application.targetFrameRate = targetFrameRate;
            Screen.sleepTimeout = useScreenTimeout ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

            // Initialize Core Services if needed
            // For example, finding or creating SceneLoader
            if (FindAnyObjectByType<SceneLoader>() == null)
            {
                GameObject sl = new GameObject("SceneLoader", typeof(SceneLoader));
                // Add any UI overlay if needed here
            }

            Debug.Log("<color=cyan>AppBootstrap: System Initialized.</color>");
        }
    }
}
