using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Convoy
{
    [System.Serializable]
    public class EventResponse
    {
        public GameEvent eventToListen;
        public UnityEvent response;
    }

    public class GameEventListener : MonoBehaviour
    {
        public List<EventResponse> events = new List<EventResponse>();

        public void OnReceiveEvent(GameEvent gameEvent)
        {
            EventResponse eResponse = null;

            for (int i = 0; i < events.Count; i++)
            {
                EventResponse e = events[i];

                if (e.eventToListen == gameEvent)
                {
                    eResponse = e;
                    break;
                }
            }

            if (eResponse != null)
            {
                eResponse.response.Invoke();
            }
        }

        private void OnEnable()
        {
            //register listener

            for (int i = 0; i < events.Count; i++)
            {
                EventResponse e = events[i];

                e.eventToListen.AddListener(this);
            }
        }

        private void OnDisable()
        {
            //unregister listener

            for (int i = 0; i < events.Count; i++)
            {
                EventResponse e = events[i];

                e.eventToListen.RemoveListener(this);
            }
        }
    }
}

