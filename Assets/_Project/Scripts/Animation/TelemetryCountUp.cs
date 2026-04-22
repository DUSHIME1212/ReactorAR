using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Reactor.Animation
{
    public class TelemetryCountUp : MonoBehaviour
    {
        public TextMeshProUGUI textLabel;
        public string format = "{0:F1}";
        public float duration = 1f;

        private float _currentValue = 0f;

        public void SetValue(float newValue)
        {
            DOTween.To(() => _currentValue, x => {
                _currentValue = x;
                textLabel.text = string.Format(format, _currentValue);
            }, newValue, duration).SetEase(Ease.OutCubic);
        }
    }
}
