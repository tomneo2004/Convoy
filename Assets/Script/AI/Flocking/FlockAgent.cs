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

        [Header("GizmoDraw")]
        public bool drawVelocity = false;
        public float drawLength = 5f;
        public Color drawVelocityColor = Color.black;

        public bool drawScanRadius = false;
        public Color drawScanColor = Color.green;

        public bool drawSeparationRadius = false;
        public Color drawSeparationColor = Color.red;

        [Header("Flock agent properties")]
        /// <summary>
        /// The raidus of scan for nearby agent
        /// who will be consider as member group
        /// </summary>
        [Tooltip("Radius for scanning neighbour agent")]
        [Range(0f, Mathf.Infinity)]
        public float scanRadius = 2f;

        /// <summary>
        /// Separation radius
        /// </summary>
        [Tooltip("Separation radius\n" +
            "Must be smaller than scan radius")]
        [Range(0f, Mathf.Infinity)]
        public float separationRadius = 1f;

        [Tooltip("How much agent can be affected by separation\n")]
        [Range(0f, Mathf.Infinity)]
        public float separationWeight = 1f;

        [Tooltip("How much agent can be affected by cohesion")]
        [Range(0f, Mathf.Infinity)]
        public float cohesionWeight = 1f;

        [Tooltip("How much agent can be affected by alignment")]
        [Range(0f, Mathf.Infinity)]
        public float alignmentWeight = 1f;


        /// <summary>
        /// Velocity of this agent
        /// </summary>
        Vector2 velocity = Vector2.zero;

        /// <summary>
        /// Get velocity of this agent
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
        }

        /// <summary>
        /// Max speed
        /// </summary>
        public float maxSpeed = 1f;

        /// <summary>
        /// How fast can this agent move
        /// </summary>
        public float speed = 1f;


        // Use this for initialization
        void Start()
        {
            //float angle = Random.Range(0, Mathf.PI * 2);
            //velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
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

        /// <summary>
        /// Get steering velocity
        /// Return flocking velocity
        /// </summary>
        /// <returns></returns>
        public Vector2 GetSteerVelocity()
        {
            //Get neighbours
            List<FlockAgent> neighbours = monitor.GetNearbyAgent(transform.position, scanRadius, this);

            if (neighbours.Count == 0)
            {
                velocity = Vector2.zero;
                return velocity;
            }
                

            Vector2 alignment = CalculateAlignment(neighbours);
            Vector2 Cohesion = CalculateCohesion(neighbours);
            Vector2 separation = CalculateSeparation(neighbours);

            //calculate amount of acceleration
            Vector2 avgDir = alignment * alignmentWeight + Cohesion * cohesionWeight + separation * separationWeight;
            Vector2 acceleration = avgDir * speed;

            //add acceleration to velocity
            velocity += acceleration;

            //limit velocity
            if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
                velocity = velocity.normalized * maxSpeed;

            return velocity;
        }

        /// <summary>
        /// Calculate alignment and return normalized direction
        /// </summary>
        /// <param name="neighbours"></param>
        /// <returns></returns>
        Vector2 CalculateAlignment(List<FlockAgent> neighbours)
        {
           Vector2 avgAlignment = Vector2.zero;

            for(int i=0; i<neighbours.Count; i++)
            {
                FlockAgent otherAgent = neighbours[i];

                avgAlignment += otherAgent.Velocity;
            }

            avgAlignment /= neighbours.Count;

            avgAlignment.Normalize();

            return avgAlignment;
        }

        /// <summary>
        /// Calculate cohesion and return normalized direction
        /// </summary>
        /// <param name="neighbours"></param>
        /// <returns></returns>
        Vector2 CalculateCohesion(List<FlockAgent> neighbours)
        {
            Vector2 avgCohesion = Vector2.zero;

            for (int i = 0; i < neighbours.Count; i++)
            {
                FlockAgent agent = neighbours[i];

                Vector2 agentPos = agent.transform.position;
                avgCohesion += agentPos;
            }

            if(neighbours.Count > 0)
            {
                avgCohesion /= neighbours.Count;

                Vector2 pos = transform.position;
                Vector2 dir = avgCohesion - pos;

                dir.Normalize();

                return dir;
            }

            return Vector2.zero;

        }

        /// <summary>
        /// Calcualte separation and return normalized direction
        /// </summary>
        /// <param name="neighbours"></param>
        /// <returns></returns>
        Vector2 CalculateSeparation(List<FlockAgent> neighbours)
        {
            Vector2 avgSeparation = Vector2.zero;

            if (neighbours.Count == 0)
                return avgSeparation;

            int count = 0;

            for (int i = 0; i < neighbours.Count; i++)
            {
                FlockAgent agent = neighbours[i];

                Vector2 agentPos = agent.transform.position;
                Vector2 diff = agentPos - new Vector2(transform.position.x, transform.position.y);
                float dist = Vector2.Distance(agentPos, transform.position);

                if(diff.sqrMagnitude > 0f && dist < (separationRadius + agent.separationRadius))
                {
                    diff = diff / diff.sqrMagnitude;
                    avgSeparation += diff;

                    count++;
                }
                
            }

            //inverse vector
            avgSeparation *= -1f;

            avgSeparation /= count;

            avgSeparation.Normalize();

            return avgSeparation;
        }

        private void OnDrawGizmos()
        {
            
            if(drawVelocity)
            {
                Gizmos.color = drawVelocityColor;
                Vector3 vec = velocity.normalized ;
                Gizmos.DrawLine(transform.position, transform.position + (vec * drawLength));

                

                
            }

            if (drawSeparationRadius)
            {
                Gizmos.color = drawSeparationColor;
                Gizmos.DrawWireSphere(transform.position, separationRadius);
            }

            if (drawScanRadius)
            {
                Gizmos.color = drawScanColor;
                Gizmos.DrawWireSphere(transform.position, scanRadius);
            }
        }

    }
}

