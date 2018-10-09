using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class PlaceableSquare : PlaceablePoint
    {
        [Tooltip("Touch area radius")]
        public float radius = 1f;
        protected Rect effectRect = new Rect();


        protected void updateSquare()
        {
            effectRect.center = transform.position;
            effectRect.xMin = effectRect.center.x - radius;
            effectRect.yMin = effectRect.center.y + radius;
            effectRect.xMax = effectRect.center.x + radius;
            effectRect.yMax = effectRect.center.y - radius;
        }

        public override bool ContainPoint(Vector2 point)
        {
            updateSquare();

            if (point != null && effectRect.Contains(point, true))
            {
                return true;
            }
                
            return false;
        }

        protected override void DrawDebug()
        {
            updateSquare();

            Gizmos.color = debugDrawColor;
            Gizmos.DrawLine(new Vector2(effectRect.xMin, effectRect.yMin), new Vector2(effectRect.xMax, effectRect.yMin));
            Gizmos.DrawLine(new Vector2(effectRect.xMax, effectRect.yMin), new Vector2(effectRect.xMax, effectRect.yMax));
            Gizmos.DrawLine(new Vector2(effectRect.xMax, effectRect.yMax), new Vector2(effectRect.xMin, effectRect.yMax));
            Gizmos.DrawLine(new Vector2(effectRect.xMin, effectRect.yMax), new Vector2(effectRect.xMin, effectRect.yMin));
        }

        public override void OnCursorHover()
        {
            
        }

        public override void OnCursorLeave()
        {
            
        }
    }
}

