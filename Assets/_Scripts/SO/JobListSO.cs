using System.Collections.Generic;
using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "New Job List", menuName = "Jobs/Job List")]
    public class JobListSO : ScriptableObject
    {
        public List<JobSO> availableJobs = new();
    }
}