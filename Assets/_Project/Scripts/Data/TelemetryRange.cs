namespace Reactor.Data
{
    [System.Serializable]
    public class TelemetryRange
    {
        public string label;    // "RPM", "bar", "°C"
        public float minValue;
        public float maxValue;
        public float targetValue;
    }
}
