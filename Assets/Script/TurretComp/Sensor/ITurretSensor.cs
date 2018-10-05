using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public interface ITurretSensor
    {
        T PickTarget<T>();
        List<T> PickTargets<T>(int num);
        bool IsTargetInRange(GameObject target);
    }
}

