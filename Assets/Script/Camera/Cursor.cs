using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class Cursor : MonoBehaviour
    {
        [Tooltip("Cursor moved event")]
        public GameEvent evt_CursorMoved;

        Vector2 lastCursorPos;

        Camera cam;

        public Vector2 LastCursorPosition
        {
            get { return lastCursorPos; }
        }

        // Use this for initialization
        void Start()
        {
            cam = GetComponent<Camera>();

            lastCursorPos = GetCursorPositionOnScreen();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 newPos = GetCursorPositionOnScreen();

            if(lastCursorPos != newPos)
            {
                if(evt_CursorMoved != null)
                {
                    evt_CursorMoved.PostEvent(this);
                }

                lastCursorPos = newPos;

            }
        }

        /// <summary>
        /// Return cursor position on screen
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCursorPositionOnScreen()
        {
            return Input.mousePosition;
        }

        /// <summary>
        /// Return cursor position in game world
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCursorPositionInWorld()
        {
            return cam.ScreenToWorldPoint(GetCursorPositionOnScreen());
        }
    }
}


