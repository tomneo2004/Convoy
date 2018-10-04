using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [CreateAssetMenu(menuName ="Team/TeamType")]
    public class TeamType : ScriptableObject
    {
        public List<TeamType> enemyTeam = new List<TeamType>();

        public bool IsEnemy(TeamType team)
        {
            return enemyTeam.Contains(team);
        }
    }
}

