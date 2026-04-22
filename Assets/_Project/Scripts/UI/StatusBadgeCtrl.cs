using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace Reactor.UI
{
    public class StatusBadgeCtrl : MonoBehaviour
    {
        public CanvasGroup badgeGroup;
        public TextMeshProUGUI label;
        public Image background;

        public void Show(string text, Color color)
        {
            label.text = text;
            background.color = color;
            
            badgeGroup.alpha = 0;
            badgeGroup.transform.localScale = Vector3.one * 0.7f;
            
            DOTween.To(() => badgeGroup.alpha, x => badgeGroup.alpha = x, 1f, 0.3f);
            badgeGroup.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            DOTween.To(() => badgeGroup.alpha, x => badgeGroup.alpha = x, 0f, 0.2f);
        }
    }
}
