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

        public bool drawObstacleRay = false;
        public Color drawRayColor = Color.yellow;

        [Header("Flock agent properties")]

        /// <summary>
        /// When enabled agent will maintain last velocity while no nearby agent detected
        /// 
        /// When disable agent's velocity will be zero until nearby agent detected
        /// </summary>
        [Tooltip("When enabled agent will maintain last velocity while no nearby agent detected\n" +
            "When disable agent's velocity will be zero until nearby agent detected")]
        public bool maintainVelocity = false;

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

        [Tooltip("How much an agent can be affected by separation\n")]
        [Range(0f, Mathf.Infinity)]
        public float separationWeight = 1f;

        [Tooltip("How much an agent can be affected by cohesion")]
        [Range(0f, Mathf.Infinity)]
        public float cohesionWeight = 1f;

        [Tooltip("How much an agent can be affected by alignment")]
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

        [Header("Obstalce bounce")]

        /// <summary>
        /// Layer of obstacle to be checked
        /// </summary>
        [Tooltip("Layer of obstacle to be checked")]
        public LayerMask obstalceLayer;

        /// <summary>
        /// Length of raycast
        /// </summary>
        [Tooltip("Length of ray casted from agent")]
        public float rayLength = 2f;

        /// <summary>
        ///  Steering weight of obstalce bounce
        /// </summary>
        [Tooltip("How much an agent can be affected by bouncing away from obstacle\n" +
            "This value must greater than separation weight in order to correct bounce from obstacle")]
        public float bounceWeight = 3f;


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

        Vector2 LimitVelocity(Vector2 vec, float speed)
        {
            if (vec.sqrMagnitude > speed * speed)
                vec = vec.normalized * speed;

            return vec;
        }

        Vector2 updateVelocity(Vector2 acceleration)
        {
            //add acceleration to velocity
            velocity += acceleration;

            //limit velocity
            velocity = LimitVelocity(velocity, maxSpeed);

            return velocity;
        }

        /// <summary>
        /// Get steering velocity
        /// Target is where agent need to travel to while no flocking
        /// Return flocking velocity
        /// </summary>
        /// <returns></returns>
        public Vector2 GetSteerVelocity(Vector2 target)
        {
            //Get neighbours
            List<FlockAgent> neighbours = monitor.GetNearbyAgent(transform.position, scanRadius, this);

            //find bounce direction
            Vector2 avgObstcaleBounceDir = CalculateObstacleBounce() * bounceWeight;

            //if need to bounce 
            if (avgObstcaleBounceDir != Vector2.zero)
            {
                Vector2 acceleration = avgObstcaleBounceDir * speed;

                return updateVelocity(acceleration);
            }

            //we have neighbours 
            if (neighbours.Count > 0)
            {

                Vector2 alignment = CalculateAlignment(neighbours);
                Vector2 Cohesion = CalculateCohesion(neighbours);
                Vector2 separation = CalculateSeparation(neighbours);

                //calculate amount of acceleration
                Vector2 avgDir = alignment * alignmentWeight + Cohesion * cohesionWeight + separation * separationWeight;
                Vector2 acceleration = (avgDir + avgObstcaleBounceDir) * speed;

                return updateVelocity(acceleration);
            }

            

            //no neighbours no bounce
            //If need to maintain last velocity
            if (maintainVelocity)
            {
                return velocity;
            }

            //clear velocity
            //velocity = Vector2.zero;
            Vector2 dirToTarget = target - new Vector2(transform.position.x, transform.position.y);
            velocity = dirToTarget.normalized * maxSpeed;
            return velocity;

            
        }

        struct RayCastInfo
        {
            /// <summary>
            /// Origin of ray
            /// </summary>
            public Vector2 origin;

            /// <summary>
            /// Direction of ray
            /// </summary>
            public Vector2 direction;

            /// <summary>
            /// Distance of ray
            /// </summary>
            public float distance;

            public RayCastInfo(Vector2 rayOrigin, Vector2 rayDirection, float rayDistance)
            {
                origin = rayOrigin;
                direction = rayDirection;
                distance = rayDistance;
            }
        }

        /// <summary>
        /// Calculate average of obstacle bouncing direction and normalized
        /// </summary>
        /// <returns></returns>
        Vector2 CalculateObstacleBounce()
        {

            //direction in average of bouncing from obstacle
            Vector2 avgBounceDir = Vector2.zero;

            //normalized velocity
            Vector2 velocityNormalized = velocity.normalized;

            //vector point to right of velocity direction
            Vector2 velocityRightDir = new Vector2(velocityNormalized.y, velocityNormalized.x * -1f);

            //agent transform poisiton in 2d
            Vector2 transPos = transform.position;

            //position on right from middle of agent
            Vector2 rightPos = velocityRightDir * separationRadius + transPos;

            //position on left from middle of agent
            Vector2 leftPos = velocityRightDir * -1f * separationRadius + transPos;

            //ray info for casting ray
            RayCastInfo[] rayInfo = new RayCastInfo[3];

            //ray from middle
            rayInfo[0] = new RayCastInfo(transPos, velocityNormalized, rayLength);

            //ray from right
            rayInfo[1] = new RayCastInfo(rightPos, velocityNormalized, rayLength);

            //ray from left
            rayInfo[2] = new RayCastInfo(leftPos, velocityNormalized, rayLength);

            for(int i=0; i<rayInfo.Length; i++)
            {
                RayCastInfo info = rayInfo[i];
                RaycastHit2D hitInfo = Physics2D.Raycast(info.origin, info.direction, info.distance);

                if(hitInfo.collider != null &&
                    ((1 << hitInfo.collider.gameObject.layer) & obstalceLayer.value) != 0)
                {
                    avgBounceDir += Vector2.Reflect(info.direction, hitInfo.normal);
                }
            }

            return avgBounceDir.normalized;
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

            if (drawVelocity)
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

            if(drawObstacleRay)
            {
                Gizmos.color = drawRayColor;
                Vector3 vRight = new Vector3(velocity.normalized.y, velocity.normalized.x * -1f, 0f);
                Vector3 right = vRight * separationRadius + transform.position;
                Vector3 left = vRight * -1f * separationRadius + transform.position;

                Vector3 end = new Vector3(velocity.normalized.x, velocity.normalized.y, 0f) * rayLength;
                Gizmos.DrawLine(right, right + end);
                Gizmos.DrawLine(left, left + end);
                Gizmos.DrawLine(transform.position, transform.position + end);
            }

        }

    }
}

