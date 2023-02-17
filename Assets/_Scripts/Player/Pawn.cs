using TMPro;
using UnityEngine;

namespace BWV
{
    public class Pawn : MonoBehaviour
    {
        #region Fields
        public PawnSO pawnData;
        public GameObject pawnMesh;
        public DistanceTraveled distanceTraveled;
        public TMP_Text pawnTag;
        public Goals pawnGoals;
        public PawnStats pawnStats;
        public Job pawnJob;
        public float spentTime;
        public float maxTime;
        public float TotalTime { get { return spentTime + distanceTraveled.distanceTraveled; } }
        public float RemainingTime { get { return maxTime - TotalTime; } }
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            distanceTraveled = GetComponent<DistanceTraveled>();
            pawnStats = GetComponent<PawnStats>();
        }
        private void Start()
        {
            this.name = "Player -> " + pawnData.name;            
            pawnMesh.GetComponent<MeshRenderer>().material.color = pawnData.playerColor;
            pawnTag.text = pawnData.name;
        }
        #endregion

        #region Custom Methods
        public void SetStartingPosition(Vector3 startPosition)
        {
            pawnData.startingPosition = startPosition;
        }

        public void MoveStartingPosition()
        {
            this.transform.position = pawnData.startingPosition;
        }

        public void IsActive(bool active)
        {
            this.gameObject.SetActive(active);
        }

        public void CountDistance(bool active)
        {
            distanceTraveled.countingDistance = active;
            distanceTraveled.distanceTraveled = 0f;
        }

        public bool SpendTime(float time)
        {
            if (time > RemainingTime) return false;

            spentTime += time;
            return true;
        }
        #endregion
    }
}
