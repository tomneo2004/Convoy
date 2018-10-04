using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [CreateAssetMenu(menuName ="Projectile/Movement")]
    public class ProjectileMove : ScriptableObject
    {
        public float timeToReachTarget = 1f;
    }
}

