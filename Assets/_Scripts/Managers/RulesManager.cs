using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections.Generic;


namespace BWV
{
    public class RulesManager : MonoBehaviour
    {
        #region Events
        public static event UnityAction OnTurnEnd;
        public static event UnityAction OnTurnStart;
        #endregion

        #region Fields

        public float maxMovement = 400f;
        public int turnCount = 0;
        public static PawnStats pawnStats;

        private Vector3 startingPosition;
        private int playerInTurn = 0;
        private GameObject pawnInTurn;
        #endregion

        #region UnityCallbacks
        private void OnEnable()
        {
            TurnPanel.OnCloseTurnPanel += TurnStart;
            PlayersManager.OnPlayersSetup += GameStart;
        }
        private void OnDisable()
        {
            TurnPanel.OnCloseTurnPanel -= TurnStart;
            PlayersManager.OnPlayersSetup -= GameStart;
        }

        void Update()
        {
            if (GameState.IsInGame || GameState.IsPaused)
            {
                UIManager.Inst.RefreshTimeSlider(maxMovement, pawnStats.TotalTime);

                if (pawnStats.TotalTime > maxMovement)
                {
                    TurnEnd();
                }
            }          
        }
        #endregion

        #region Custom Methods

        public void GameStart()
        {
            SetupPawnInTurn();
            TurnStart();
        }
        void TurnStart()
        {
            GameState.InGame();
            UIManager.Inst.statsPanel.RefreshStats(pawnStats.stats);
            pawnInTurn.transform.position = startingPosition;
            pawnStats.MoveStartingPosition();
            pawnStats.IsActive(true);
            pawnStats.CountDistance(true);
        }
        void TurnEnd()
        {
            pawnStats.CountDistance(false);
            pawnStats.IsActive(false);
            GameState.Pause();            
            playerInTurn = (playerInTurn + 1) % PlayersManager.playersIngame.Count;
            if (playerInTurn == 0) turnCount++;
            SetupPawnInTurn();
            OnTurnEnd();
        }

        void SetupPawnInTurn()
        {
            pawnInTurn = PlayersManager.playersIngame[playerInTurn];
            pawnStats = pawnInTurn.GetComponent<PawnStats>();
            pawnStats.maxTime = maxMovement;            
            pawnStats.spentTime = 0;
        }
        #endregion
    }
}
