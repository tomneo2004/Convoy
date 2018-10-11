using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [CreateAssetMenu(menuName ="RunTimeSet/FlockAgentMonitor")]
    public class FlockAgentMonitor : Monitor<FlockAgent>
    {
        /// <summary>
        /// Get all nearby flock agnet from position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public List<FlockAgent> GetNearbyAgent(Vector2 position, float radius, FlockAgent agent)
        {
            List<FlockAgent> agents = new List<FlockAgent>();

            if (objects.Count <= 0)
                return agents;

            IEnumerator ie = objects.GetEnumerator();
            while(ie.MoveNext())
            {
                FlockAgent anAgent = (FlockAgent)ie.Current;

                if (anAgent == agent)
                    continue;

                float dist = Vector2.Distance(position, anAgent.transform.position);

                if (dist < (radius + anAgent.scanRadius))
                    agents.Add(anAgent);
            }

            return agents;
        }
    }
}

