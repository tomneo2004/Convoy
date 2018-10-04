using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Convoy
{
    [CreateAssetMenu(menuName ="Event/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        HashSet<GameEventListener> listenerSet = new HashSet<GameEventListener>();
        UnityEngine.Object eventPoster;

        public void PostEvent(UnityEngine.Object poster)
        {
            eventPoster = poster;

            IEnumerator ie = listenerSet.GetEnumerator();
            while(ie.MoveNext())
            {
                GameEventListener listener = (GameEventListener)ie.Current;

                if (listener != null)
                    listener.OnReceiveEvent(this);
            }

            eventPoster = null;
        }

        public void AddListener(GameEventListener listener)
        {
            listenerSet.Add(listener);
        }

        public void RemoveListener(GameEventListener listener)
        {
            listenerSet.Remove(listener);
        }
        
        public T GetEventPoster<T>()
        {
            if (eventPoster == null)
                return default(T);

            return (T)Convert.ChangeType(eventPoster, typeof(T));
        }
    }
}


