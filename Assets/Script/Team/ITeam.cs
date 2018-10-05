using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public interface ITeam
    {
        /// <summary>
        /// Give a list of team member and find out all enemies
        /// </summary>
        /// <param name="unknowTeam"></param>
        /// <returns></returns>
        List<Team> FindEnemyMembers(List<Team> unknowTeam);

        /// <summary>
        /// Which team this belong to
        /// </summary>
        /// <returns></returns>
        TeamType GetTeamType();
    }
}
