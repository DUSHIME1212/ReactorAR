using UnityEngine;

namespace Reactor.Data
{
    [CreateAssetMenu(menuName = "Reactor/AppSettings")]
    public class AppSettings : ScriptableObject
    {
        [Header("AR Configuration")]
        public float planeMinDetectionTime = 2.0f;
        public Color placementValidColor = new Color(0.22f, 0.54f, 0.87f, 0.8f);
        public Color placementInvalidColor = new Color(0.87f, 0.22f, 0.22f, 0.8f);

        [Header("UI Aesthetics")]
        public Color primaryAccent = new Color(0.12f, 0.62f, 0.94f, 1f);
        public float defaultFadeDuration = 0.3f;
        
        [Header("Debug")]
        public bool showDebugHUD = false;
    }
}
