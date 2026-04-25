using UnityEngine;
using System.Collections.Generic;

namespace Reactor.Data
{
    [CreateAssetMenu(fileName = "ScenarioDatabase", menuName = "Reactor/ScenarioDatabase")]
    public class ScenarioDatabase : ScriptableObject
    {
        public List<TrainingScenario> scenarios = new List<TrainingScenario>();
    }
}
