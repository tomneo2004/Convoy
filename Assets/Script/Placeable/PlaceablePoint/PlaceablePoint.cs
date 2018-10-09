using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public enum PlaceableState
    {
        Free,
        Ocuppied
    }

    public class PlaceablePoint : AbstractPlaceable
    {
        /// <summary>
        /// Transform of object that occupied this placeable
        /// </summary>
        protected Transform occupiedObject;

        /// <summary>
        /// Return a vector of position in 2d
        /// </summary>
        public Vector2 GetPosition2D
        {
            get { return transform.position; }
        }

        public bool drawDebug = true;
        public Color debugDrawColor = Color.white;


        /// <summary>
        /// State of placeable
        /// </summary>
        protected PlaceableState state = PlaceableState.Free;

        void OnDrawGizmos()
        {
            if (drawDebug)
                DrawDebug();
        }

        /// <summary>
        /// Place an object at this placeable
        /// </summary>
        /// <param name="transform"></param>
        public virtual void PlaceObject(Transform transform)
        {
            occupiedObject = transform;
            transform.position = transform.position;
            state = PlaceableState.Ocuppied;
        }

        /// <summary>
        /// Free object from this placeable
        /// </summary>
        public virtual void FreeObject()
        {
            occupiedObject = null;
            state = PlaceableState.Free;
        }

        /// <summary>
        /// Is this placeable occupied
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOccupied()
        {
            return (state == PlaceableState.Ocuppied) && (occupiedObject != null);
        }

        public override bool ContainPoint(Vector2 point)
        {
            if(point != null)
            {
                Vector2 position = transform.position;

                return position == point;
            }

            return false;
        }

        public override void OnCursorHover()
        {
            //Nothing to do
        }

        public override void OnCursorLeave()
        {
            //Nothing to do
        }

        protected virtual void DrawDebug()
        {
            //Nothing to do
        }
    }
}


