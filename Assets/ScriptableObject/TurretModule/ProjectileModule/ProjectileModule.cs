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
        /// Magzine size
        /// </summary>
        [Tooltip("Number of ammo to shoot before reload \n Magzine size")]
        public int shotPerFire = 1;

        /// <summary>
        /// Number of ammo to shoot in each fire
        /// Example shotgun
        /// </summary>
        [Tooltip("Number of ammo to shoot in each fire \n example shotgun")]
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
        /// Projectile max spread angle
        /// </summary>
        [Tooltip("Projectile max spread angle")]
        public float maxSpreadAngle = 10f;

        /// <summary>
        /// Accuracy 1 most accurate otherwise 0 or value in between
        /// </summary>
        [Tooltip("Accuracy 1 most accurate otherwise 0 or value in between")]
        [Range(0f, 1f)]
        public float accuracyFactor = 1f;

        /// <summary>
        /// Projectile to shoot
        /// </summary>
        [Tooltip("Projectile to shoot")]
        public GameObject projectile;

        public override void Initialize(GameObject gameObject)
        {
        }
    }
}

