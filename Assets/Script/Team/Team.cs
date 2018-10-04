using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class Team : MonoBehaviour, ITeam
    {
        public TeamType teamType;

        public List<Team> FindEnemyMembers(List<Team> unknowTeam)
        {
            List<Team> enemyTeam = new List<Team>();

            for(int i=0; i<unknowTeam.Count; i++)
            {
                Team t = unknowTeam[i];

                if (teamType.IsEnemy(t.teamType))
                    enemyTeam.Add(t);

            }

            return enemyTeam;
        }

        public TeamType GetTeamType()
        {
            return teamType;
        }
    }
}

