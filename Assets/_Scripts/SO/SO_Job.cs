using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Jobs", menuName = "Jobs")]
public class SO_Jobs : ScriptableObject
{
    [SerializeField] private Dictionary<string, int> jobWages = new Dictionary<string, int>();

    public int GetWage(string jobName)
    {
        if (jobWages.ContainsKey(jobName))
        {
            return jobWages[jobName];
        }
        else
        {
            Debug.LogError($"Job '{jobName}' does not exist!");
            return 0;
        }
    }
}