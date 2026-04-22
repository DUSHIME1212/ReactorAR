using UnityEngine;
using Reactor.Data;

namespace Reactor.Training
{
    public class ScenarioLoader : MonoBehaviour
    {
        public static TrainingScenario ActiveScenario { get; private set; }

        public static void SetActiveScenario(TrainingScenario scenario)
        {
            ActiveScenario = scenario;
        }

        public void LoadScenarioFromResources(string path)
        {
            var scenario = Resources.Load<TrainingScenario>(path);
            if (scenario != null)
            {
                SetActiveScenario(scenario);
            }
            else
            {
                Debug.LogError($"Scenario at {path} not found in Resources.");
            }
        }
    }
}
