using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using DG.Tweening;
using TMPro;

namespace Reactor.UI
{
    public class ScanningHUD : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public RectTransform diamondIndicator;   // the diamond/rotated-square on floor
    public Image diamondImage;
    public TextMeshProUGUI scanLabel;
    public CanvasGroup statusRow;
    public Button startScenarioBtn;          // appears after plane found

    private Tween _pulseTween;
    private bool  _found;

    void Update()
    {
        if (_found) return;

        // Poll for planes. This is more robust than inconsistent events across ARF versions.
        if (planeManager.trackables.count > 0)
        {
            OnPlanesFound();
        }
    }

    void OnEnable()
    {
        statusRow.alpha = 0;
        DOTween.To(() => statusRow.alpha, x => statusRow.alpha = x, 1f, 0.4f).SetDelay(0.3f);
        StartDiamondPulse();
        startScenarioBtn.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        _pulseTween?.Kill();
    }

    void StartDiamondPulse()
    {
        _pulseTween = diamondIndicator
            .DOScale(new Vector3(1.08f, 1.08f, 1f), 0.8f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        DOTween.To(() => diamondImage.color, x => diamondImage.color = x, new Color(diamondImage.color.r, diamondImage.color.g, diamondImage.color.b, 0.4f), 0.8f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnPlanesFound()
    {
        _found = true;
        _pulseTween?.Kill();

        // Snap to solid, scale up with bounce
        DOTween.To(() => diamondImage.color, x => diamondImage.color = x, new Color(diamondImage.color.r, diamondImage.color.g, diamondImage.color.b, 0.8f), 0.3f);
        diamondIndicator.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
        diamondImage.color = new Color(0.22f, 0.54f, 0.87f, 0.8f);

        DOTween.To(() => scanLabel.color, x => scanLabel.color = x, new Color(scanLabel.color.r, scanLabel.color.g, scanLabel.color.b, 0f), 0.2f).OnComplete(() => {
            scanLabel.text = "Floor detected — tap to place";
            DOTween.To(() => scanLabel.color, x => scanLabel.color = x, new Color(scanLabel.color.r, scanLabel.color.g, scanLabel.color.b, 1f), 0.3f);
        });

        // Show start button
        startScenarioBtn.gameObject.SetActive(true);
        startScenarioBtn.transform.localScale = Vector3.zero;
        startScenarioBtn.transform
            .DOScale(1f, 0.4f).SetEase(Ease.OutBack).SetDelay(0.2f);
    }
}}
