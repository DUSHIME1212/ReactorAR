using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Reactor.UI
{
    public class ParameterPanelCtrl : MonoBehaviour
{
    [Header("Panel")]
    public CanvasGroup panelGroup;
    public RectTransform panelRect;
    public Image blurOverlay;           // semi-transparent dark Image simulating blur

    [Header("Controls")]
    public Slider rpmSlider;
    public TextMeshProUGUI rpmValueLabel;
    public TextMeshProUGUI paramNameLabel;  // "STIRRER"
    public Toggle onToggle;
    public Button applyBtn;
    public Button closeBtn;

    [Header("Events")]
    public System.Action<string, float, bool> OnApply;

    string _currentParam;

    void Awake()
    {
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelRect.localScale = Vector3.one * 0.85f;

        rpmSlider.onValueChanged.AddListener(v => {
            rpmValueLabel.text = Mathf.RoundToInt(v).ToString();
        });

        applyBtn.onClick.AddListener(Apply);
        closeBtn.onClick.AddListener(Hide);
    }

    public void Show(string paramName, float currentValue, float min, float max)
    {
        _currentParam = paramName;
        paramNameLabel.text = paramName;
        rpmSlider.minValue = min;
        rpmSlider.maxValue = max;
        rpmSlider.value    = currentValue;
        rpmValueLabel.text = Mathf.RoundToInt(currentValue).ToString();

        panelGroup.interactable = true;

        // Panel scale + fade in
        DOTween.Sequence()
            .Append(DOTween.To(() => blurOverlay.color, x => blurOverlay.color = x, new Color(blurOverlay.color.r, blurOverlay.color.g, blurOverlay.color.b, 0.65f), 0.2f))
            .Join(panelRect.DOScale(1f, 0.35f).SetEase(Ease.OutBack))
            .Join(DOTween.To(() => panelGroup.alpha, x => panelGroup.alpha = x, 1f, 0.25f));
    }

    public void Hide()
    {
        panelGroup.interactable = false;
        DOTween.Sequence()
            .Append(panelRect.DOScale(0.85f, 0.25f).SetEase(Ease.InBack))
            .Join(DOTween.To(() => panelGroup.alpha, x => panelGroup.alpha = x, 0f, 0.2f))
            .Join(DOTween.To(() => blurOverlay.color, x => blurOverlay.color = x, new Color(blurOverlay.color.r, blurOverlay.color.g, blurOverlay.color.b, 0f), 0.2f));
    }

    void Apply()
    {
        // Punch the APPLY button
        applyBtn.transform.DOPunchScale(Vector3.one * 0.12f, 0.3f, 5)
            .OnComplete(() => {
                OnApply?.Invoke(_currentParam, rpmSlider.value, onToggle.isOn);
                Hide();
            });
    }
}}
