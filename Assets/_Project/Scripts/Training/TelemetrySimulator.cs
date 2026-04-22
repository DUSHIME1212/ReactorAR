using System.Collections;
using UnityEngine;
using Reactor.Data;
using Reactor.UI;

namespace Reactor.Training
{
    public class TelemetrySimulator : MonoBehaviour
    {
        public float noiseMagnitude = 0.05f;
        public float updateInterval = 0.5f;

        private TelemetryHUDCtrl _hud;
        private TelemetryRange[] _activeRanges;
        private Coroutine _simulationCoroutine;

        private void Awake()
        {
            _hud = FindAnyObjectByType<TelemetryHUDCtrl>();
        }

        public void StartSimulation(TelemetryRange[] ranges)
        {
            _activeRanges = ranges;
            if (_simulationCoroutine != null) StopCoroutine(_simulationCoroutine);
            _simulationCoroutine = StartCoroutine(SimulateRoutine());
        }

        public void StopSimulation()
        {
            if (_simulationCoroutine != null) StopCoroutine(_simulationCoroutine);
        }

        private IEnumerator SimulateRoutine()
        {
            while (true)
            {
                if (_activeRanges != null && _hud != null)
                {
                    // Create a copy of ranges with slight noise
                    TelemetryRange[] noisyRanges = new TelemetryRange[_activeRanges.Length];
                    for (int i = 0; i < _activeRanges.Length; i++)
                    {
                        var r = _activeRanges[i];
                        noisyRanges[i] = new TelemetryRange
                        {
                            label = r.label,
                            targetValue = r.targetValue + Random.Range(-noiseMagnitude, noiseMagnitude)
                        };
                    }
                    _hud.AnimateTo(noisyRanges);
                }
                yield return new WaitForSeconds(updateInterval);
            }
        }
    }
}
