using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public abstract class TurretComponent : MonoBehaviour
    {
        /// <summary>
        /// Initialize turret component
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Active turret component
        /// </summary>
        public abstract void Active();

        /// <summary>
        /// Deactive turret component
        /// </summary>
        public abstract void Deactive();
    }
}

