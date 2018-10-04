using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public abstract class TurretModule : ScriptableObject
    {
        public abstract void Initialize(TurretBase turret);
    }
}

