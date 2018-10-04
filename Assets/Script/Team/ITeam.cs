using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public interface ITeam
    {
        List<Team> FindEnemyMembers(List<Team> unknowTeam);
        TeamType GetTeamType();
    }
}
