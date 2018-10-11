using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class FlockAgent : MonoBehaviour
    {
        /// <summary>
        /// The monitor that monitoring this agent
        /// </summary>
        public FlockAgentMonitor monitor;

        /// <summary>
        /// The raidus of scan for nearby agent
        /// who will be consider as member group
        /// </summary>
        public float scanRadius = 2f;

        protected Vector2 avgAlignment = Vector2.zero;
        protected Vector2 avgCohesion = Vector2.zero;
        protected Vector2 avgSeparation = Vector2.zero;

        protected Rigidbody2D agentRigidbody;

        // Use this for initialization
        void Start()
        {
            agentRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            monitor.Add(this);
        }

        private void OnDisable()
        {
            monitor.Remove(this);
        }

        public Vector2 GetSteer()
        {
            List<FlockAgent> neighbours = monitor.GetNearbyAgent(transform.position, scanRadius, this);

            if (neighbours.Count == 0)
                return Vector2.zero;

            //CalculateAlignment(neighbours);
            //CalculateCohesion(neighbours);
            CalculateSeparation(neighbours);

            return avgAlignment + avgCohesion + avgSeparation;
        }

        void CalculateAlignment(List<FlockAgent> neighbours)
        {
            avgAlignment = Vector2.zero;

            if (agentRigidbody == null)
                return;

            for(int i=0; i<neighbours.Count; i++)
            {
                FlockAgent agent = neighbours[i];

                avgAlignment += agent.GetComponent<Rigidbody2D>().velocity;
            }

            avgAlignment /= neighbours.Count;
        }

        void CalculateCohesion(List<FlockAgent> neighbours)
        {
            avgCohesion = Vector2.zero;

            for (int i = 0; i < neighbours.Count; i++)
            {
                FlockAgent agent = neighbours[i];

                Vector2 agentPos = agent.transform.position;
                avgCohesion += agentPos;
            }

            avgCohesion /= neighbours.Count;
            avgCohesion = avgCohesion - new Vector2(transform.position.x, transform.position.y);

        }

        void CalculateSeparation(List<FlockAgent> neighbours)
        {
            avgSeparation = Vector2.zero;

            for (int i = 0; i < neighbours.Count; i++)
            {
                FlockAgent agent = neighbours[i];

                Vector2 agentPos = agent.transform.position;
                Vector2 diff = new Vector2(transform.position.x, transform.position.y) - agentPos;
                diff /= diff.sqrMagnitude;
                avgSeparation += diff;
            }

            avgSeparation /= neighbours.Count;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, scanRadius);
        }
    }
}

