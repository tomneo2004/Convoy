using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class PlaceableUnit : PlaceableSquare
    {
        public SpriteRenderer indicator;

        private void Start()
        {
            
        }

        public void OnUnitDisabled(GameEvent gameEvent)
        {
            Transform trans = gameEvent.GetEventPoster<Unit>().transform;

            if (trans == occupiedObject)
                FreeObject();
        }

        public override void OnCursorHover()
        {
            base.OnCursorHover();

            if(state == PlaceableState.Free)
            {
                Color newColor = indicator.color;
                newColor.a = 1f;
                indicator.color = newColor; 
            }
                
        }

        public override void OnCursorLeave()
        {
            base.OnCursorLeave();

            if (state == PlaceableState.Free)
            {
                Color newColor = indicator.color;
                newColor.a = 0.5f;
                indicator.color = newColor;
            }
        }
                
    }

}
