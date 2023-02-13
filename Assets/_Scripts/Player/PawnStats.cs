using TMPro;
using UnityEngine;

namespace BWV
{
    public class PawnStats : MonoBehaviour
    {
        public SO_Pawn pawnData;
        public GameObject pawnMesh;
        public TMP_Text pawnTag;
        public PlayerStats stats;

        private void Start()
        {
            this.name = "Player -> " + pawnData.name;
            pawnMesh.GetComponent<MeshRenderer>().material.color = pawnData.playerColor;
            pawnTag.text = pawnData.name;
        }
    }    
}
