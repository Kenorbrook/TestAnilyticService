using UnityEngine;

namespace Analytics
{
    public class AnalyticService : MonoBehaviour
    {
        private static AnalyticService _instance;
        private static bool isInit;

        private static AnalyticSender _analyticSender;
        private static TrackAwaiter _trackAwaiter;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(gameObject);
            else if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public static void Initialize(string serverUrl, float cooldownBeforeSend = 3)
        {
            if(isInit) return;
            isInit = true;
            _trackAwaiter = new TrackAwaiter(cooldownBeforeSend, _instance);
            _analyticSender = new AnalyticSender(serverUrl, _trackAwaiter);
        }

        internal static void TrackEvent(string type, string data)
        {
            _trackAwaiter.DoAwaitStart();
            _analyticSender.Track(type, data);
        }
    }
}