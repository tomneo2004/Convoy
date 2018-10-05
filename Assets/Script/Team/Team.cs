using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class Team : MonoBehaviour, ITeam
    {
        /// <summary>
        /// Define which team
        /// </summary>
        [Tooltip("Define which team")]
        public TeamType teamType;

        public List<Team> FindEnemyMembers(List<Team> unknowTeam)
        {
            List<Team> enemyMembers = new List<Team>();

            for(int i=0; i<unknowTeam.Count; i++)
            {
                Team t = unknowTeam[i];

                if (teamType.IsEnemy(t.teamType))
                    enemyMembers.Add(t);

            }

            return enemyMembers;
        }

        public TeamType GetTeamType()
        {
            return teamType;
        }
    }
}

