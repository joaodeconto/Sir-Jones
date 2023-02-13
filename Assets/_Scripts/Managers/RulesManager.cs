using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections.Generic;


namespace BWV
{
    public class RulesManager : MonoBehaviour
    {
        public static event UnityAction OnTurnEnd;
        public static event UnityAction OnTurnStart;
        
        public float maxMovement = 400f;
        public int turnCount = 0;

        public NavMeshAgent agent;
        public DistanceTraveled distanceTraveled;
        public PawnStats pawnStats;
        private Vector3 startingPosition;
        private int playerInTurn = 0;
        public GameObject pawnInTurn;

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
            if (pawnInTurn != null)
            {
                UIManager.Inst.RefreshTimeSlider(maxMovement, pawnStats.TotalTime);

                if (pawnStats.TotalTime > maxMovement)
                {
                    TurnEnd();
                }
            }          
        }
        public void GameStart()
        {
            SetupPawnInTurn();            
            startingPosition = agent.transform.position;
            TurnStart();
        }
        void TurnStart()
        {
            GameState.InGame();
            UIManager.Inst.statsPanel.RefreshStats(pawnStats.stats);
            pawnInTurn.transform.position = startingPosition;
            distanceTraveled.countingDistance = true;
            distanceTraveled.distanceTraveled = 0f;
            pawnInTurn.SetActive(true);
        }

        void TurnEnd()
        {
            pawnInTurn.SetActive(false);
            pawnStats.distanceTraveled.countingDistance = false;
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
            distanceTraveled = pawnInTurn.GetComponent<DistanceTraveled>();
            agent = pawnInTurn.GetComponent<PawnMovement>().agent;
            pawnStats.spentTime = 0;
        }


        void GameStateHandle(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.InGame:
                    agent.gameObject.SetActive(true);
                    break;
                case GameState.State.MenuStart:
                    agent.gameObject.SetActive(false);
                    break;
                case GameState.State.Wait:
                    agent.gameObject.SetActive(false);
                    break;
                default:
                    agent.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
