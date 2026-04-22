using UnityEngine;
using DG.Tweening;
using Reactor.Core;

namespace Reactor.UI
{
    public class BootScreen : MonoBehaviour
    {
        public CanvasGroup logoGroup;
        public CanvasGroup backgroundGroup;
        public float delayBeforeLoad = 2f;
        public string nextScene = "HomeMenu";

        private void Start()
        {
            // Initial state
            logoGroup.alpha = 0;
            logoGroup.transform.localScale = Vector3.one * 0.8f;

            // Sequential animation
            Sequence bootSeq = DOTween.Sequence();
            bootSeq.Append(logoGroup.DOFade(1f, 0.6f))
                   .Join(logoGroup.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBack))
                   .AppendInterval(delayBeforeLoad)
                   .Append(backgroundGroup.DOFade(0f, 0.5f))
                   .OnComplete(() => SceneLoader.Load(nextScene));
        }
    }
}
