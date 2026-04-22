using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Reactor.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public CanvasGroup loadingOverlay;
        public float fadeDuration = 0.5f;

        private static SceneLoader _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (loadingOverlay != null)
                loadingOverlay.alpha = 0;
        }

        public static void Load(string sceneName)
        {
            _instance.StartCoroutine(_instance.LoadRoutine(sceneName));
        }

        private IEnumerator LoadRoutine(string sceneName)
        {
            if (loadingOverlay != null)
            {
                yield return DOTween.To(() => loadingOverlay.alpha, x => loadingOverlay.alpha = x, 1f, fadeDuration).WaitForCompletion();
            }

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            while (!op.isDone)
            {
                yield return null;
            }

            if (loadingOverlay != null)
            {
                yield return DOTween.To(() => loadingOverlay.alpha, x => loadingOverlay.alpha = x, 0f, fadeDuration).WaitForCompletion();
            }
        }
    }
}
