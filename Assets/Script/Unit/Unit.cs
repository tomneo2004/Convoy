using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [RequireComponent(typeof(Team))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Unit : MonoBehaviour
    {
        public GameEvent unityDestroy;

        float _angle;
        Vector2 center;
        // Use this for initialization
        void Start()
        {
            center = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 newPos = Vector2.zero;
            _angle += 1f * Time.deltaTime;
            newPos = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * 3f;
            transform.position = center + newPos;
        }

        private void OnDisable()
        {
            unityDestroy.PostEvent(this);
      
        }
    }
}

