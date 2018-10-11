using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Convoy
{
    [RequireComponent(typeof(FlockAgent))]
    //[RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(AIMovement))]
    public class AIControl : MonoBehaviour
    {
        public Transform target;
        //Seeker pathSeeker;
        Path path;
        FlockAgent flockAgent;
        AIMovement movement;

        public float speed = 20f;

        // Use this for initialization
        void Start()
        {
           // pathSeeker = GetComponent<Seeker>();
            flockAgent = GetComponent<FlockAgent>();
            movement = GetComponent<AIMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 steer = flockAgent.GetSteer();
            steer.Normalize();
            //Vector2 dir = new Vector2(target.transform.position.x, target.transform.position.y) - 
                //new Vector2(transform.position.x, transform.position.y);

            

            GetComponent<Rigidbody2D>().velocity += steer * speed * 20f * Time.deltaTime;

            if (steer != Vector2.zero)
            {
                Vector2 dir = GetComponent<Rigidbody2D>().velocity;
                dir.Normalize();

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle -= 90f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 vec = GetComponent<Rigidbody2D>().velocity;
            vec.Normalize();
            Gizmos.DrawLine(transform.position, transform.position+(vec * 5f));
        }
    }
}

