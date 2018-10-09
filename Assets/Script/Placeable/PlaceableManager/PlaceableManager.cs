using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class PlaceableManager : MonoBehaviour
    {
        public PlaceablePoint[] placeables;

        /// <summary>
        /// Get placeable point. Point is in world position
        /// </summary>
        /// <param name="point"> 
        /// Point where in world position 
        /// </param>
        /// <returns></returns>
        public PlaceablePoint GetPlaceablePoint(Vector2 point)
        {
            for(int i=0; i<placeables.Length; i++)
            {
                if (placeables[i].ContainPoint(point))
                    return placeables[i];
            }

            return null;
        }
    }
}

