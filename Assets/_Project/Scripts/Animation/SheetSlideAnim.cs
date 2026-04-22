using UnityEngine;
using DG.Tweening;

namespace Reactor.Animation
{
    public class SheetSlideAnim : MonoBehaviour
    {
        public RectTransform sheet;
        public float hiddenY = -1000f;
        public float visibleY = 0f;
        public float duration = 0.5f;
        public Ease ease = Ease.OutCubic;

        public void Open()
        {
            DOTween.To(() => sheet.anchoredPosition, x => sheet.anchoredPosition = x, new Vector2(sheet.anchoredPosition.x, visibleY), duration).SetEase(ease);
        }

        public void Close()
        {
            DOTween.To(() => sheet.anchoredPosition, x => sheet.anchoredPosition = x, new Vector2(sheet.anchoredPosition.x, hiddenY), duration).SetEase(Ease.InCubic);
        }
    }
}
