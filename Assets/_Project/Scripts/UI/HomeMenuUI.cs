using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Reactor.Data;

namespace Reactor.UI
{
    /// <summary>
    /// Populates and animates the Home Menu.
    /// Works in two modes:
    ///   1) With a ScenarioDatabase — reads data and pushes it to ScenarioCardButton on each card.
    ///   2) Without a database — cards must have their scenario field set directly in the Inspector.
    /// No dynamic instantiation needed — cards already exist in the scene.
    /// </summary>
    public class HomeMenuUI : MonoBehaviour
    {
        [Header("Nav Tabs")]
        public Button[] navButtons;
        public Image[]  navUnderlines;
        public int      defaultTab = 1;

        [Header("Scenario Data (optional — assigns data to cards automatically)")]
        public ScenarioDatabase database;

        [Header("Scenario Cards (assign in Inspector — must already exist in scene)")]
        public RectTransform   scenarioListContainer;
        public RectTransform[] scenarioCards;

        void Start()
        {
            // Step 1: push scenario data from database onto cards if available
            if (database != null && database.scenarios != null && database.scenarios.Count > 0)
            {
                for (int i = 0; i < scenarioCards.Length && i < database.scenarios.Count; i++)
                {
                    var btn = scenarioCards[i].GetComponent<ScenarioCardButton>();
                    if (btn != null)
                        btn.Initialize(database.scenarios[i]);
                    else
                        Debug.LogWarning($"[HomeMenuUI] ScenarioCard at index {i} has no ScenarioCardButton component.");
                }
            }
            else
            {
                Debug.LogWarning("[HomeMenuUI] No ScenarioDatabase assigned — cards must have scenario set in Inspector.");
            }

            // Step 2: fix the layout container anchors so it fills the space correctly
            FixContainerAnchors();

            // Step 3: fix each card's layout
            FixCardAnchors();

            // Step 4: animate cards in
            AnimateCardsIn();

            // Step 5: wire nav tabs
            for (int i = 0; i < navButtons.Length; i++)
            {
                int idx = i;
                navButtons[i].onClick.AddListener(() => SelectTab(idx));
            }
            SelectTab(defaultTab);
        }

        void FixContainerAnchors()
        {
            if (scenarioListContainer == null) return;
            scenarioListContainer.anchorMin = new Vector2(0f, 0f);
            scenarioListContainer.anchorMax = new Vector2(1f, 1f);
            scenarioListContainer.pivot     = new Vector2(0.5f, 0.5f);
            // Adjusted insets: Header is usually ~180, Nav bar is ~120. 
            // We use slightly smaller numbers to bring the UI together.
            scenarioListContainer.offsetMin = new Vector2(24f,  120f); // Bottom margin (above nav)
            scenarioListContainer.offsetMax = new Vector2(-24f, -180f); // Top margin (below header)
        }

        void FixCardAnchors()
        {
            // Fix VerticalLayoutGroup on the container
            if (scenarioListContainer != null)
            {
                var vlg = scenarioListContainer.GetComponent<VerticalLayoutGroup>();
                if (vlg != null)
                {
                    vlg.childForceExpandWidth  = true;
                    vlg.childForceExpandHeight = false;
                    vlg.childControlWidth      = true;
                    vlg.childControlHeight     = true;
                    vlg.spacing                = 16f; // Tighter spacing
                    vlg.padding                = new RectOffset(16, 16, 16, 16); // Smaller padding
                    vlg.childAlignment         = TextAnchor.UpperCenter;
                }

                // Ensure the container itself doesn't grow infinitely (disable ContentSizeFitter)
                var csf = scenarioListContainer.GetComponent<ContentSizeFitter>();
                if (csf != null) csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            }

            foreach (var card in scenarioCards)
            {
                if (card == null) continue;

                // Make sure LayoutElement gives card a fixed height
                var le = card.GetComponent<LayoutElement>();
                if (le == null) le = card.gameObject.AddComponent<LayoutElement>();
                le.preferredHeight = 180f;
                le.minHeight       = 180f;
                le.flexibleHeight  = 0f;
                le.flexibleWidth   = 1f;

                // Ensure CanvasGroup exists for fade animation
                var cg = card.GetComponent<CanvasGroup>();
                if (cg == null) cg = card.gameObject.AddComponent<CanvasGroup>();
            }
        }

        void AnimateCardsIn()
        {
            for (int i = 0; i < scenarioCards.Length; i++)
            {
                var card = scenarioCards[i];
                if (card == null) continue;

                var cg = card.GetComponent<CanvasGroup>();
                if (cg) cg.alpha = 0f;
                card.localScale = new Vector3(0.92f, 0.92f, 1f);

                float delay = 0.15f + i * 0.12f;
                if (cg)
                    DOTween.To(() => cg.alpha, x => cg.alpha = x, 1f, 0.4f).SetDelay(delay);
                card.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(delay);
            }
        }

        void SelectTab(int index)
        {
            for (int i = 0; i < navUnderlines.Length; i++)
            {
                bool  active = (i == index);
                var   img    = navUnderlines[i];
                Color c      = img.color;
                DOTween.To(() => img.color, x => img.color = x,
                    new Color(c.r, c.g, c.b, active ? 1f : 0f), 0.2f);
                navButtons[i].transform.DOScale(active ? 1.1f : 1f, 0.2f);
            }
        }
    }
}