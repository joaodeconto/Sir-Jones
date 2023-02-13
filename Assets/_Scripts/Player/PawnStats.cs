using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

namespace BWV
{
    public class PawnStats : MonoBehaviour
    {
        public SO_Pawn pawnData;
        public GameObject pawnMesh;
        public DistanceTraveled distanceTraveled;
        public TMP_Text pawnTag;
        public PlayerStats stats;
        public float spentTime;
        public float maxTime;
        public float TotalTime { get { return spentTime + distanceTraveled.distanceTraveled; } }
        public float RemainingTime { get { return maxTime - TotalTime; } }

        private void Start()
        {
            this.name = "Player -> " + pawnData.name;
            distanceTraveled = GetComponent<DistanceTraveled>();
            pawnMesh.GetComponent<MeshRenderer>().material.color = pawnData.playerColor;
            pawnTag.text = pawnData.name;
        }

        public bool SpendTime(float time)
        {
            if (time > RemainingTime) return false;

            spentTime += time;
            return true;
        }
    }    
}
