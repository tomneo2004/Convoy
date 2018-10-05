using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TurretSensor : TurretComponent, ITurretSensor
    {
        /// <summary>
        /// Define which team will be detected by sensor
        /// </summary>
        [Tooltip("Define which team will be detected by sensor")]
        public List<TeamType> enemyList = new List<TeamType>();

        //Sensor Area
        CircleCollider2D sensorTrigger;
        
        //All targets in sensor area
        protected HashSet<GameObject> targetMem = new HashSet<GameObject>();

        public override void Initialize()
        {
            sensorTrigger = GetComponent<CircleCollider2D>();
            sensorTrigger.isTrigger = true;
            sensorTrigger.enabled = false;
        }

        public override void Active()
        {
            sensorTrigger.enabled = true;
        }

        public override void Deactive()
        {
            sensorTrigger.enabled = false;
            targetMem.Clear();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ITeam team = collision.GetComponent<ITeam>();

            if(team != null)
            {
                if (enemyList.Contains(team.GetTeamType()))
                    RememberTarget(collision.gameObject);
            }
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ForgetTarget(collision.gameObject);
        }

        private void RememberTarget(GameObject target)
        {
            targetMem.Add(target);
        }

        private void ForgetTarget(GameObject target)
        {
            targetMem.Remove(target);
        }

        List<T> GetAllTargets<T>()
        {
            List<T> result = new List<T>();

            IEnumerator ie = targetMem.GetEnumerator();
            while (ie.MoveNext())
            {
                T comp = ((GameObject)ie.Current).GetComponent<T>();

                if (!comp.Equals(default(T)))
                    result.Add(comp);
            }

            return result;
        }

        public virtual bool IsTargetInRange(GameObject target)
        {
            return targetMem.Contains(target);
        }

        public virtual T PickTarget<T>()
        {
            if(targetMem.Count > 0)
            {
                IEnumerator ie = targetMem.GetEnumerator();
                ie.MoveNext();

                T comp = ((GameObject)ie.Current).GetComponent<T>();
                if (!comp.Equals(default(T)))
                    return comp;
            }

            return default(T);
        }

        public virtual List<T> PickTargets<T>(int num)
        {
            return GetAllTargets<T>();
        }

        public void OnUnitDestroy(GameEvent gameEvent)
        {
            GameObject o = gameEvent.GetEventPoster<Unit>().gameObject;
            targetMem.Remove(o);
            Debug.Log(o.name + " remove from memory");
        }
    }
}
