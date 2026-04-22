using UnityEngine;

namespace Reactor.Data
{
    [System.Serializable]
    public class TrainingStep
    {
        public string stepLabel;          // "TURN ON THE POWER"
        public GameObject modelPrefab;    // swapped into world each step
        public TelemetryRange[] telemetry;
        public string statusBadge;        // "POWER TRANSFERRED" etc.
    }
}
