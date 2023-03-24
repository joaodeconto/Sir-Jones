using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace BWV
{
    public class JobManager : MonoBehaviour
    {
        public static JobListSO jobList;
        public static Dictionary<Pawn, Job> takenJobs = new Dictionary<Pawn, Job>();

        public static bool TryAssignJob(Pawn pawn, JobSO job)
        {
            if (takenJobs.ContainsKey(pawn))
            {
                Debug.LogWarning("Pawn already has a job!");
                return false;
            }

            if (CanApplyForJob(pawn.pawnStats, job))
            {
                takenJobs.Add(pawn, new Job(pawn, job));
                Debug.Log($"{pawn.name} got the job {job.name}!");
                return true;
            }
            else
            {
                Debug.LogWarning($"{pawn.name} cannot get the job {job.name}!");
                return false;
            }
        }

        public static void QuitJob(Pawn pawn)
        {
            if (takenJobs.TryGetValue(pawn, out Job job))
            {
                takenJobs.Remove(pawn);
                Debug.Log($"{pawn.name} quit the job {job.jobSO.name}!");
            }
        }

        private static bool CanApplyForJob(PawnStats stats, JobSO job)
        {
            foreach (KeyValuePair<StatsTypes, int> requiredStat in job.requiredStats.statsDictionary)
            {
                if (stats.GetStat(requiredStat.Key) < requiredStat.Value)
                {
                    return false;
                }
            }
            Debug.Log("GOT JOB " + job.jobName);
            return true;
        }
        
        //TODO List negative motives
        private static string CanApplyForJobReturnString(PawnStats stats, JobSO job)
        {
            string requirementsNotMet = "";
            if (stats.GetStat(StatsTypes.Gold) < job.requiredStats.GetStat(StatsTypes.Gold))
            {
                requirementsNotMet += "Not enough gold. ";
            }
            if (stats.GetStat(StatsTypes.Dexterity) < job.requiredStats.GetStat(StatsTypes.Dexterity))
            {
                requirementsNotMet += "Not enough dexterity. ";
            }
            if (stats.GetStat(StatsTypes.Morale) < job.requiredStats.GetStat(StatsTypes.Morale))
            {
                requirementsNotMet += "Low morale. ";
            }
            if (stats.GetStat(StatsTypes.Health) < job.requiredStats.GetStat(StatsTypes.Health))
            {
                requirementsNotMet += "Poor health. ";
            }
            if (stats.GetStat(StatsTypes.Nobility) < job.requiredStats.GetStat(StatsTypes.Nobility))
            {
                requirementsNotMet += "Not noble enough. ";
            }
            if (stats.GetStat(StatsTypes.Charisma) < job.requiredStats.GetStat(StatsTypes.Charisma))
            {
                requirementsNotMet += "Not charismatic enough. ";
            }
            if (stats.GetStat(StatsTypes.Strength) < job.requiredStats.GetStat(StatsTypes.Strength))
            {
                requirementsNotMet += "Not strong enough. ";
            }
            if (stats.GetStat(StatsTypes.Experience) < job.requiredStats.GetStat(StatsTypes.Experience))
            {
                requirementsNotMet += "Not experienced enough. ";
            }
            return requirementsNotMet;
        }
    }
}
