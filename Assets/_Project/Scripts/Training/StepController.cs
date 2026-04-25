using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Reactor.Data;
using Reactor.UI;

namespace Reactor.Training
{
    public class StepController : MonoBehaviour
{
    [Header("Data")]
    public TrainingScenario scenario;

    [Header("UI")]
    public TextMeshProUGUI stepNumberLabel;   // "STEP 1 / 5"
    public TextMeshProUGUI stepTitleLabel;    // "TURN ON THE POWER"
    public TextMeshProUGUI statusBadgeLabel;
    public CanvasGroup statusBadge;
    public Button nextBtn;
    public CanvasGroup instructionPanel;

    [Header("World")]
    public Transform modelAnchor;
    public TelemetryHUDCtrl telemetryHUD;
    public ModelSwapManager modelSwap;

    int _currentStep = -1;

    void Start()
    {
        if (scenario == null)
            scenario = ScenarioLoader.ActiveScenario;

        nextBtn.onClick.AddListener(AdvanceStep);

        // Slide instruction panel in from top
        var rt = instructionPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, 60f);
        instructionPanel.alpha = 0;
        DOTween.Sequence()
            .Append(DOTween.To(() => rt.anchoredPosition, x => rt.anchoredPosition = x, Vector2.zero, 0.4f).SetEase(Ease.OutCubic))
            .Join(DOTween.To(() => instructionPanel.alpha, x => instructionPanel.alpha = x, 1f, 0.3f))
            .SetDelay(0.5f);

        AdvanceStep();
    }

    public void AdvanceStep()
    {
        _currentStep++;
        if (_currentStep >= scenario.steps.Length) return;

        var step = scenario.steps[_currentStep];

        // Punch scale on step label
        stepNumberLabel.text = $"STEP {_currentStep + 1} / {scenario.steps.Length}";
        stepNumberLabel.transform.DOPunchScale(Vector3.one * 0.18f, 0.35f, 5);

        // Title cross-fade
        DOTween.To(() => stepTitleLabel.color, x => stepTitleLabel.color = x, new Color(stepTitleLabel.color.r, stepTitleLabel.color.g, stepTitleLabel.color.b, 0f), 0.15f).OnComplete(() => {
            stepTitleLabel.text = step.stepLabel;
            DOTween.To(() => stepTitleLabel.color, x => stepTitleLabel.color = x, new Color(stepTitleLabel.color.r, stepTitleLabel.color.g, stepTitleLabel.color.b, 1f), 0.25f);
        });

        // 3D model swap
        modelSwap.SwapTo(step.modelPrefab);

        // Telemetry update
        telemetryHUD.AnimateTo(step.telemetry);

        // Status badge
        if (!string.IsNullOrEmpty(step.statusBadge))
            ShowStatusBadge(step.statusBadge);

        // NEXT button: hide on last step
        nextBtn.gameObject.SetActive(_currentStep < scenario.steps.Length - 1);
    }

    void ShowStatusBadge(string text)
    {
        statusBadgeLabel.text = text;
        statusBadge.alpha = 0;
        statusBadge.transform.localScale = Vector3.one * 0.7f;
        DOTween.Sequence()
            .Append(DOTween.To(() => statusBadge.alpha, x => statusBadge.alpha = x, 1f, 0.3f))
            .Join(statusBadge.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack));
    }
}}
