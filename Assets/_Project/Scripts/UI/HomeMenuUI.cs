using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Reactor.UI
{
    public class HomeMenuUI : MonoBehaviour
{
    [Header("Nav")]
    public Button[] navButtons;
    public Image[]  navUnderlines;
    public int      defaultTab = 1;       // Simulator tab active

    [Header("Scenario List")]
    public RectTransform[] scenarioCards;

    void Start()
    {
        // Stagger scenario cards falling in from above
        for (int i = 0; i < scenarioCards.Length; i++)
        {
            var card = scenarioCards[i];
            var cg   = card.GetComponent<CanvasGroup>();
            var origY = card.anchoredPosition.y;

            card.anchoredPosition = new Vector2(card.anchoredPosition.x, origY + 40f);
            if (cg) cg.alpha = 0;

            DOTween.To(() => card.anchoredPosition, x => card.anchoredPosition = x, new Vector2(card.anchoredPosition.x, origY), 0.4f)
                .SetEase(Ease.OutCubic)
                .SetDelay(0.1f + i * 0.08f);

            if (cg)
                DOTween.To(() => cg.alpha, x => cg.alpha = x, 1f, 0.35f).SetDelay(0.1f + i * 0.08f);
        }

        // Nav tab setup
        for (int i = 0; i < navButtons.Length; i++)
        {
            int idx = i;
            navButtons[i].onClick.AddListener(() => SelectTab(idx));
        }
        SelectTab(defaultTab);
    }

    void SelectTab(int index)
    {
        for (int i = 0; i < navUnderlines.Length; i++)
        {
            bool active = (i == index);
            int idx = i;
            var img = navUnderlines[idx];
            DOTween.To(() => img.color, x => img.color = x, new Color(img.color.r, img.color.g, img.color.b, active ? 1f : 0f), 0.2f);
            
            navButtons[i].transform
                .DOScale(active ? 1.1f : 1f, 0.2f);
        }
    }
}}
