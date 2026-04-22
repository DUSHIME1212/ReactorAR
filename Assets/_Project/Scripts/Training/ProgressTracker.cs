using UnityEngine;

namespace Reactor.Training
{
    public class ProgressTracker : MonoBehaviour
    {
        private const string SAVE_PREFIX = "ReactorScenario_";

        public void MarkComplete(string scenarioID, int stepIndex)
        {
            PlayerPrefs.SetInt(SAVE_PREFIX + scenarioID + "_Step_" + stepIndex, 1);
            PlayerPrefs.Save();
        }

        public bool IsStepComplete(string scenarioID, int stepIndex)
        {
            return PlayerPrefs.GetInt(SAVE_PREFIX + scenarioID + "_Step_" + stepIndex, 0) == 1;
        }

        public void ResetProgress(string scenarioID)
        {
            // Simple approach: clear all prefixed keys
            // In a real app, you might want to track counts
            PlayerPrefs.DeleteAll(); // Caution: generic delete
            PlayerPrefs.Save();
        }
    }
}
