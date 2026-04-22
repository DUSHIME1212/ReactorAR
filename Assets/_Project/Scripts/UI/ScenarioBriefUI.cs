using UnityEngine;
using TMPro;
using Reactor.Data;

namespace Reactor.UI
{
    public class ScenarioBriefUI : MonoBehaviour
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI subtitleText;
        public TextMeshProUGUI stepCountText;

        public void Populate(TrainingScenario scenario)
        {
            titleText.text = scenario.scenarioTitle;
            subtitleText.text = scenario.scenarioSubtitle;
            stepCountText.text = $"{scenario.steps.Length} Steps";
        }
    }
}
