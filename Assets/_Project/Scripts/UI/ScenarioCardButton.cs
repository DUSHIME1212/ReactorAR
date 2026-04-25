using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Reactor.Data;
using Reactor.Core;
using Reactor.Training;

namespace Reactor.UI
{
    /// <summary>
    /// Attach this to each ScenarioCard root object.
    /// Handles: click → load scenario → transition to 03_Simulation.
    /// Also fixes its own anchors/size at runtime so cards stretch full-width.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ScenarioCardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Scenario")]
        public TrainingScenario scenario;
        public string simulationSceneName = "03_Simulation";

        [Header("Card Text (optional — auto-found if blank)")]
        public TextMeshProUGUI titleLabel;
        public TextMeshProUGUI subtitleLabel;
        public TextMeshProUGUI stepCountLabel;

        [Header("Visual")]
        public Image cardBackground;
        public Color normalColor  = new Color(0.13f, 0.14f, 0.18f, 1f);
        public Color hoverColor   = new Color(0.17f, 0.20f, 0.28f, 1f);
        public Color pressedColor = new Color(0.10f, 0.16f, 0.26f, 1f);

        private Button _btn;
        private RectTransform _rt;

        void Awake()
        {
            _btn = GetComponent<Button>();
            _rt  = GetComponent<RectTransform>();

            _btn.onClick.AddListener(OnClick);

            if (cardBackground) cardBackground.color = normalColor;
        }

        void Start()
        {
            if (scenario != null)
                Initialize(scenario);
        }

        /// <summary>
        /// Called when dynamicially instantiating cards from HomeMenuUI.
        /// </summary>
        public void Initialize(TrainingScenario newScenario)
        {
            scenario = newScenario;

            // Apply text (with fallbacks for debugging)
            if (titleLabel)    titleLabel.text    = string.IsNullOrEmpty(scenario.scenarioTitle) ? "[MISSING TITLE]" : scenario.scenarioTitle;
            if (subtitleLabel) subtitleLabel.text = string.IsNullOrEmpty(scenario.scenarioSubtitle) ? "No description provided." : scenario.scenarioSubtitle;
            if (stepCountLabel) stepCountLabel.text = $"{(scenario.steps != null ? scenario.steps.Length : 0)} Steps";

            // Ensure the background is actually clickable
            if (cardBackground != null)
            {
                cardBackground.raycastTarget = true;
            }

            // Ensure it has a CanvasGroup and it's set to allow clicks
            var cg = GetComponent<CanvasGroup>();
            if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
            
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
            cg.interactable = true;

            // Check for EventSystem (common reason for non-clickable UI)
            if (UnityEngine.EventSystems.EventSystem.current == null)
            {
                Debug.LogError("[ScenarioCardButton] NO EVENTSYSTEM IN SCENE! UI will not be clickable.");
            }

            Debug.Log($"[ScenarioCardButton] Initialized '{scenario.scenarioTitle}' on {gameObject.name}. Clickable: {cg.blocksRaycasts}");
        }

        void OnClick()
        {
            if (scenario != null)
            {
                Debug.Log($"[ScenarioCardButton] Clicked: {scenario.scenarioTitle}");
                ScenarioLoader.SetActiveScenario(scenario);
            }

            // Punch scale for satisfying feedback
            transform.DOPunchScale(Vector3.one * 0.05f, 0.25f, 4)
                     .OnComplete(() => SceneLoader.Load(simulationSceneName));
        }

        public void OnPointerEnter(PointerEventData _)
        {
            Debug.Log($"[ScenarioCardButton] Hovering over: {scenario?.scenarioTitle}");
            if (cardBackground)
                cardBackground.DOColor(hoverColor, 0.15f);
            transform.DOScale(1.02f, 0.15f);
        }

        public void OnPointerExit(PointerEventData _)
        {
            if (cardBackground)
                cardBackground.DOColor(normalColor, 0.15f);
            transform.DOScale(1f, 0.15f);
        }
    }
}