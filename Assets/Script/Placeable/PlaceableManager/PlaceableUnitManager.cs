using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class PlaceableUnitManager : PlaceableManager
    {
        /// <summary>
        /// placeable that cursor is hover on
        /// </summary>
        PlaceablePoint currentPlaceable;

        public void OnCursorMoved(GameEvent gameEvent)
        {
            Cursor cursor = gameEvent.GetEventPoster<Cursor>();

            PlaceablePoint point = GetPlaceablePoint(cursor.GetCursorPositionInWorld());

            if (currentPlaceable != point)
            {
                if(currentPlaceable != null)
                    currentPlaceable.OnCursorLeave();

                currentPlaceable = point;
            }
                

            if(currentPlaceable != null)
            {
                currentPlaceable.OnCursorHover();
            }
        }
    }
}

