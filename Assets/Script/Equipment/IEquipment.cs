using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public interface IEquipment
    {

        /// <summary>
        /// Initialize equipment
        /// </summary>
        void Initialize();

        /// <summary>
        /// Active equipment
        /// </summary>
        void Active();

        /// <summary>
        /// Deactive equipment
        /// </summary>
        void Deactive();
    }
}


