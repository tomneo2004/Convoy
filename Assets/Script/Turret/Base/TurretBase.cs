using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class TurretBase : MonoBehaviour
    {
        /// <summary>
        /// All turret components
        /// </summary>
        protected TurretComponent[] comps;

        protected virtual void Start()
        {
            comps = GetComponents<TurretComponent>();

            //initialize components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Initialize();

            //Active components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Active();
        }

        public virtual void ActiveTurret()
        {
            //Active components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Active();
        }

        public virtual void DeactiveTurret()
        {
            //Deactive components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Deactive();
        }
    }
}

