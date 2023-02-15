using System.Collections.Generic;
using UnityEngine;

namespace BWV
{
    public class PawnStats : MonoBehaviour
    {
        public Dictionary<StatsTypes, int> stats = new Dictionary<StatsTypes, int>()
        {
            { StatsTypes.Gold, 0 },
            { StatsTypes.Dexterity, 0 },
            { StatsTypes.Strength, 0 },
            { StatsTypes.Health, 0 },
            { StatsTypes.Nobility, 0 },
            { StatsTypes.Morale, 0 },
            { StatsTypes.Charisma, 0 },
            { StatsTypes.Experience, 0 }
        };

        public void AddStat(StatsTypes statType, int amount)
        {
            stats[statType] += amount;
        }

        public void SubtractStat(StatsTypes statType, int amount)
        {
            stats[statType] -= amount;
        }

        public int GetStat(StatsTypes statType)
        {
            return stats[statType];
        }
    }
}