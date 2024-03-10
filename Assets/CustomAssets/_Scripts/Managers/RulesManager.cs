using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BWV
{
    public class RulesManager : MonoBehaviour
    {
        #region Events
        public static event UnityAction OnTurnEnd;
        #endregion

        #region Fields

        public float maxMovement = 400f;
        public int turnCount = 0;
        public static Pawn _pawn;

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

        void FixedUpdate()
        {

            if (_pawn == null || !_pawn.isActiveAndEnabled) return;

            if (GameState.IsInGame || GameState.IsPaused)
            {
                UIManager.Inst.RefreshTimeSlider(maxMovement, _pawn.TotalTime);

                if (_pawn.TotalTime > maxMovement)
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
            UIManager.Inst.statsPanel.RefreshGoals(_pawn.pawnGoals);            
            _pawn.MoveStartingPosition();
            _pawn.IsActive(true);
            _pawn.CountDistance(true);
        }
        void TurnEnd()
        {
            _pawn.CountDistance(false);
            _pawn.IsActive(false);
            _pawn = null;
            GameState.Pause();            
            playerInTurn = (playerInTurn + 1) % PlayersManager.playersIngame.Count;
            if (playerInTurn == 0) turnCount++;
            SetupPawnInTurn();
            OnTurnEnd();
        }

        void SetupPawnInTurn()
        {
            pawnInTurn = PlayersManager.playersIngame[playerInTurn];
            _pawn = pawnInTurn.GetComponent<Pawn>();
            _pawn.maxTime = maxMovement;            
            _pawn.spentTime = 0;
        }
        #endregion
    }
}
