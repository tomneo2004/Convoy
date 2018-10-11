using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class Monitor<T> : ScriptableObject
    {

        /// <summary>
        /// Objects in set to be monitored
        /// </summary>
        protected HashSet<T> objects = new HashSet<T>();

        /// <summary>
        /// Get all monitored objects
        /// </summary>
        public T[] GetObjects
        {
            get
            {
                T[] retVal = new T[objects.Count];

                objects.CopyTo(retVal);
          
                return retVal;
            }
        }

        /// <summary>
        /// Add object to be monitored
        /// </summary>
        /// <param name="obj"></param>
        public virtual void  Add(T obj)
        {
            if(obj != null)
            {
                objects.Add(obj);
            }
        }

        /// <summary>
        /// Remove object from monitor
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Remove(T obj)
        {
            if(obj != null)
            {
                objects.Remove(obj);
            }
        }
    }
}

