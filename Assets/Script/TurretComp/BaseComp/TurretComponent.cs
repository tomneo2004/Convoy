using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public abstract class TurretComponent : MonoBehaviour
    {
        public abstract void Initialize();

        public abstract void Active();

        public abstract void Deactive();
    }
}

