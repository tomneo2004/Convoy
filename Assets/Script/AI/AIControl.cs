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
            Vector2 steer = flockAgent.GetSteerVelocity();

            if (steer != Vector2.zero)
            {
                //move
                transform.position += new Vector3(steer.x, steer.y, 0f)*Time.deltaTime;
            }
            else
            {
                Vector2 tDir = target.transform.position - transform.position;

                //move
                tDir = tDir.normalized * flockAgent.speed * Time.deltaTime;
                transform.position += new Vector3(tDir.x, tDir.y, 0f);
            }

            //rotate
            Vector2 dir = steer;
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}

