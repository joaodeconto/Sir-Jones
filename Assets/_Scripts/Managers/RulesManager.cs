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
        public static Pawn _pawn;

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
            pawnInTurn.transform.position = startingPosition;
            _pawn.MoveStartingPosition();
            _pawn.IsActive(true);
            _pawn.CountDistance(true);
        }
        void TurnEnd()
        {
            _pawn.CountDistance(false);
            _pawn.IsActive(false);
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
