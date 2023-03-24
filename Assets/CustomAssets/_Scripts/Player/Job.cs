using System;

namespace BWV
{
    public class Job
    {
        public Pawn pawn;
        public JobSO jobSO;
        public float currentWage;
        public float timeWorked;
        public float minTime;

        public Job(Pawn pawn, JobSO jobSO)
        {
            this.pawn = pawn;
            this.jobSO = jobSO;
            this.currentWage = jobSO.baseWage;
            this.timeWorked = 0;
            this.minTime = jobSO.minTime;
    }

        public void Work(float time)
        {
            pawn.pawnStats.SetStat(StatsTypes.Gold, (int)(currentWage / time));
            timeWorked += time;
        }
    }
}
