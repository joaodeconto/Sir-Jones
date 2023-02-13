using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "ScriptableObjects/Pawn")]
    public class SO_Pawn : ScriptableObject
    {
        public string playerName;
        public Color playerColor; //TODO color[] pallete
        public int playerNumber;
        public int turnOrder;
        public GameObject pawnPrefab;
        public Vector3 startingPosition;
    }
}