using UnityEngine;

namespace Reactor.Data
{
    [CreateAssetMenu(menuName = "Reactor/TrainingScenario")]
    public class TrainingScenario : ScriptableObject
    {
        public string scenarioTitle;      // "Reactor Activation"
        public string scenarioSubtitle;   // "Simulation showing the steps to launch the RTU-600"
        public TrainingStep[] steps;
    }
}