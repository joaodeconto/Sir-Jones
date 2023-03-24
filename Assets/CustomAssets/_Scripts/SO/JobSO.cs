using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "Job", menuName = "Jobs/Job")]
    public class JobSO : ScriptableObject
    {
        public PawnStats requiredStats;
        public string jobName;
        public int baseWage;
        public float minTime; //minimum expected work per turn or complain
    }
}