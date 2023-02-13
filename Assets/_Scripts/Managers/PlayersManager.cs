using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BWV
{
    public class PlayersManager : MonoBehaviour
    {
        public SO_Pawn[] playersPreset;
        public static List<GameObject> playersIngame = new();
        public static event UnityAction OnPlayersSetup;

        public Vector3 startingRef;

        private void OnEnable()
        {
            StartingMenu.OnGameStart += SetupPlayers;
        }

        private void OnDisable()
        {
            StartingMenu.OnGameStart -= SetupPlayers;
        }

        void SetupPlayers(int humanPlayers, bool sirJones)
        {
            int playerCount = humanPlayers;
            //GAMBI Sir jones is player 0, so if !sirjones start on 1
            int b = sirJones ? 0 : 1;

            for (int i = b; i <= playerCount; i++)
            {
                //playersPreset[i].playerNumber = i + 1;
                //playersPreset[i].turnOrder = i;
                GameObject pawn = Instantiate(playersPreset[i].pawnPrefab, startingRef, Quaternion.identity);
                pawn.GetComponent<PawnStats>().pawnData = playersPreset[i];
                playersIngame.Add(pawn);
            }
            OnPlayersSetup();
        }
    }
}

