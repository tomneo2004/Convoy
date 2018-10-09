using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public abstract class AbstractPlaceable : MonoBehaviour
    {

        /// <summary>
        /// Check if vector2d inside this placeable object
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public abstract bool ContainPoint(Vector2 point);

        /// <summary>
        /// Tell placeable cursor is hovering on it
        /// </summary>
        public abstract void OnCursorHover();

        /// <summary>
        /// Tell placeable cursor leave 
        /// </summary>
        public abstract void OnCursorLeave();
    }
}

