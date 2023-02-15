using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BWV
{
    public class PlayersManager : MonoBehaviour
    {
        #region Fields
        public SO_Pawn[] playersPreset;
        public static List<GameObject> playersIngame = new();
        public static event UnityAction OnPlayersSetup;

        public Vector3 startingRef;
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            StartingMenu.OnGameStart += SetupPlayers;
        }

        private void OnDisable()
        {
            StartingMenu.OnGameStart -= SetupPlayers;
        }
        #endregion

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
                pawn.GetComponent<Pawn>().pawnData = playersPreset[i];
                playersIngame.Add(pawn);
            }
            OnPlayersSetup();
        }
    }
}

