using UnityEngine;
using DG.Tweening;

namespace Reactor.Animation
{
    public class DOTweenAnimator : MonoBehaviour
    {
        public enum AnimationType { Fade, Scale, Slide }

        [Header("Settings")]
        public AnimationType animationType = AnimationType.Fade;
        public float duration = 0.4f;
        public Ease ease = Ease.OutQuad;
        public float delay = 0f;
        public bool playOnEnable = false;

        private CanvasGroup _canvasGroup;
        private Vector3 _originalScale;
        private Vector2 _originalAnchoredPos;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _originalScale = transform.localScale;
            if (_rectTransform != null) _originalAnchoredPos = _rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            if (playOnEnable) Play();
        }

        public void Play()
        {
            switch (animationType)
            {
                case AnimationType.Fade:
                    if (_canvasGroup != null)
                    {
                        _canvasGroup.alpha = 0;
                        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, duration).SetEase(ease).SetDelay(delay);
                    }
                    break;
                case AnimationType.Scale:
                    transform.localScale = Vector3.zero;
                    transform.DOScale(_originalScale, duration).SetEase(ease).SetDelay(delay);
                    break;
                case AnimationType.Slide:
                    if (_rectTransform != null)
                    {
                        var startPos = _originalAnchoredPos + new Vector2(0, -50f);
                        _rectTransform.anchoredPosition = startPos;
                        DOTween.To(() => _rectTransform.anchoredPosition, x => _rectTransform.anchoredPosition = x, _originalAnchoredPos, duration).SetEase(ease).SetDelay(delay);
                    }
                    break;
            }
        }
    }
}
