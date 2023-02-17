using System.Collections.Generic;
using UnityEngine;

namespace BWV
{
    public class PawnStats: MonoBehaviour
    {
        public Dictionary<StatsTypes, int> statsDictionary = new Dictionary<StatsTypes, int>();

        public PawnStats()
        {
            foreach (StatsTypes statType in System.Enum.GetValues(typeof(StatsTypes)))
            {
                statsDictionary.Add(statType, 0);
            }
        }

        public void SetStat(StatsTypes statType, int amount)
        {
            statsDictionary[statType] = amount;
        }

        public int GetStat(StatsTypes statType)
        {
            return statsDictionary[statType];
        }
    }
}