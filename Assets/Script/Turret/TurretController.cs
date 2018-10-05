using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    /// <summary>
    /// Turret class that control turret components
    /// </summary>
    public class TurretController : MonoBehaviour, IEquipment
    {
        /// <summary>
        /// All turret components
        /// </summary>
        protected TurretComponent[] comps;


        public virtual void Initialize()
        {
            //get all turret components
            comps = GetComponents<TurretComponent>();

            //initialize components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Initialize();

            //Active components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Active();
        }

        public virtual void Active()
        {

            //Active components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Active();
        }

        public virtual void Deactive()
        {

            //Deactive components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Deactive();
        }
    }
}

