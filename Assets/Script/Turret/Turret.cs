using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class Turret : Equipment
    {
        /// <summary>
        /// All turret components
        /// </summary>
        protected TurretComponent[] comps;


        public override void Initialize()
        {
            base.Initialize();

            comps = GetComponents<TurretComponent>();

            //initialize components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Initialize();

            //Active components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Active();
        }

        public override void Active()
        {
            base.Active();

            //Active components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Active();
        }

        public override void Deactive()
        {
            base.Deactive();

            //Deactive components
            for (int i = 0; i < comps.Length; i++)
                comps[i].Deactive();
        }
    }
}

