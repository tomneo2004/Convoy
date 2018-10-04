using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [CreateAssetMenu(menuName = "TurretModule/Weapon/Cannon")]
    public class ProjectileModule : TurretModule
    {
        /// <summary>
        /// How many ammo can shoot before reload
        /// </summary>
        [Tooltip("Number of ammo to shoot before reload")]
        public int shotPerFire = 1;

        /// <summary>
        /// Number of ammo to shoot in each fire
        /// </summary>
        [Tooltip("Number of ammo to shoot in each fire")]
        public int ammoPerShot = 1;

        /// <summary>
        /// Rate of fire
        /// </summary>
        [Tooltip("Rate of fire")]
        public float fireInterval = 1f;

        /// <summary>
        /// How much time for reload
        /// </summary>
        [Tooltip("Total time for reload")]
        public float reloadTime = 3f;

        /// <summary>
        /// Speed of rotation
        /// </summary>
        [Tooltip("Speed of rotation")]
        public float turnSpeed = 10f;

        /// <summary>
        /// Attact angle range
        /// </summary>
        [Tooltip("Attact angle range")]
        public float attackAngle = 45f;

        /// <summary>
        /// Projectile to shoot
        /// </summary>
        [Tooltip("Projectile to shoot")]
        public GameObject projectile;

        public override void Initialize(TurretBase turret)
        {
        }
    }
}

