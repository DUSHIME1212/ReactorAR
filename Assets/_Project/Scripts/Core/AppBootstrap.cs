using UnityEngine;

namespace Reactor.Core
{
    public class AppBootstrap : MonoBehaviour
    {
        private static AppBootstrap _instance;

        [Header("Settings")]
        public int targetFrameRate = 60;
        public bool useScreenTimeout = false;

        [Header("Boot Flow")]
        public string nextScene = "02_HomeMenu";
        public float bootDelay = 0.5f;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            // Must use root GO since this object is a child of [GLOBAL_SYSTEMS]
            DontDestroyOnLoad(transform.root.gameObject);

            Application.targetFrameRate = targetFrameRate;
            Screen.sleepTimeout = useScreenTimeout ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

            ServiceLocator.Register<AppBootstrap>(this);
            Debug.Log("<color=cyan>AppBootstrap: System Initialized.</color>");
        }

        private void Start()
        {
            // Transition to the main menu after the boot delay
            StartCoroutine(BootRoutine());
        }

        private System.Collections.IEnumerator BootRoutine()
        {
            yield return new WaitForSeconds(bootDelay);

            if (!string.IsNullOrEmpty(nextScene))
            {
                Debug.Log($"<color=cyan>AppBootstrap: Loading {nextScene}...</color>");
                SceneLoader.Load(nextScene);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                ServiceLocator.Unregister<AppBootstrap>();
            }
        }
    }
}

