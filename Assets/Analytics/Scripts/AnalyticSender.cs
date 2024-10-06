using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Analytics
{
    internal class AnalyticSender
    {
        private const int TIMEOUT = 15;
        private const string SENDER_KEY = "SenderData";
        private const string BEFORE_SEND_KEY = "SendData";

        private readonly string _serverUrl;
        private readonly Events _events;

        internal AnalyticSender(string serverUrl, TrackAwaiter trackAwaiter)
        {
            _serverUrl = serverUrl;
            _events = new Events();
            trackAwaiter.awaitDoneAction += SendEvents;

            if (PlayerPrefs.HasKey(SENDER_KEY))
                _events.Add(JsonUtility.FromJson<Events>(PlayerPrefs.GetString(SENDER_KEY)));

            if (PlayerPrefs.HasKey(BEFORE_SEND_KEY))
                _events.Add(JsonUtility.FromJson<Events>(PlayerPrefs.GetString(BEFORE_SEND_KEY)));


            SendEvents();
        }


        public void Track(string type, string data)
        {
            _events.AddEvent(type, data);
            PlayerPrefs.SetString(BEFORE_SEND_KEY, JsonUtility.ToJson(_events));
            
        }

        private async void SendEvents()
        {
            if (_events.isEmpty) return;


            UnityWebRequest request = new UnityWebRequest(_serverUrl, "POST");
            request.timeout = TIMEOUT;
            request.SetRequestHeader("Content-Type", "application/json");

            string data = JsonUtility.ToJson(_events, true);
            
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();


            //Save local. If the request freezes and new events come, in order not to lose the old ones, you need to save them
            PlayerPrefs.SetString(SENDER_KEY, data);
            PlayerPrefs.DeleteKey(BEFORE_SEND_KEY);


            var _operation = request.SendWebRequest();

            while (!_operation.isDone)
            {
                await Task.Yield();
            }

            CheckResult(request.result);
        }

        private void CheckResult(UnityWebRequest.Result result)
        {
            switch (result)
            {
                case UnityWebRequest.Result.Success:
                    _events.Clear();
                    PlayerPrefs.DeleteKey(SENDER_KEY);
                    break;
                default:
                    //Any logger
                    Debug.LogError("Request not send");
                    break;
            }
        }


    }
    
    [Serializable]
    public class Events
    {
        public List<Event> events;
        internal bool isEmpty => events.Count == 0;

        internal Events()
        {
            events = new List<Event>();
        }

        internal void Add(Events addEvents)
        {
            if (addEvents == null) return;

            events.AddRange(addEvents.events);
        }

        internal void AddEvent(string type, string data)
        {
            events.Add(new Event(type, data));
        }

        internal void Clear()
        {
            events.Clear();
        }
    }

    [Serializable]
    public class Event
    {
        public string type;
        public string data; 

        internal Event(string type, string data)
        {
            this.data = data;
            this.type = type;
        }
    }
}