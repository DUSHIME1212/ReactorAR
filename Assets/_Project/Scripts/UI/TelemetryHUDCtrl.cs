using UnityEngine;
using DG.Tweening;
using TMPro;
using Reactor.Data;

namespace Reactor.UI
{
    public class TelemetryHUDCtrl : MonoBehaviour
{
    [System.Serializable]
    public class TelemetryBadge
    {
        public string key;                  // "RPM", "bar", "°C"
        public TextMeshProUGUI valueLabel;
        public TextMeshProUGUI unitLabel;
        public RectTransform badgeRoot;
    }

    public TelemetryBadge[] badges;

    float[] _currentValues;

    void Awake()
    {
        _currentValues = new float[badges.Length];
    }

    public void AnimateTo(TelemetryRange[] ranges)
    {
        foreach (var range in ranges)
        {
            for (int i = 0; i < badges.Length; i++)
            {
                if (badges[i].key != range.label) continue;

                int idx = i;
                float from = _currentValues[idx];
                float to   = range.targetValue;
                _currentValues[idx] = to;

                // DOTween numeric count-up
                DOTween.To(
                    () => from,
                    v => {
                        badges[idx].valueLabel.text = v.ToString("F1");
                    },
                    to,
                    1.2f
                ).SetEase(Ease.OutCubic);

                // Badge pop
                badges[idx].badgeRoot
                    .DOPunchScale(Vector3.one * 0.12f, 0.4f, 4);

                badges[idx].unitLabel.text = range.label;
            }
        }
    }
}}
