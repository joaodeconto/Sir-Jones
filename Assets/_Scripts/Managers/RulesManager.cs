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

        public NavMeshAgent agent;
        public DistanceTraveled distanceTraveled;
        public float maxMovement = 400f;
        public int turnCount = 0;

        private Vector3 startingPosition;
        private int playerInTurn = 0;
        private GameObject pawnInTurn;
        private bool pawnActive;

        private void OnEnable()
        {
            TurnPanel.OnCloseTurnPanel += TurnStart;
            PlayersManager.OnPlayersSetup += GameStart;
            //GameState.OnStateChange += GameStateHandle;
        }
        void Start()
        {
           
        }
        private void OnDisable()
        {
            TurnPanel.OnCloseTurnPanel -= TurnStart;
            //GameState.OnStateChange -= GameStateHandle;
        }

        void Update()
        {
            if (pawnInTurn != null)
            {
                if ( GameState.IsInGame && distanceTraveled.distanceTraveled > maxMovement)
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
            pawnInTurn.transform.position = startingPosition;
            distanceTraveled.countingDistance = true;
            distanceTraveled.distanceTraveled = 0f;
            pawnInTurn.SetActive(true);
        }

        void TurnEnd()
        {
            pawnInTurn.SetActive(false);
            distanceTraveled.countingDistance = false;
            GameState.Pause();            
            playerInTurn = (playerInTurn + 1) % PlayersManager.playersIngame.Count;
            if (playerInTurn == 0) turnCount++;
            SetupPawnInTurn();
            OnTurnEnd();
        }

        void SetupPawnInTurn()
        {
            pawnInTurn = PlayersManager.playersIngame[playerInTurn];
            agent = pawnInTurn.GetComponent<PawnMovement>().agent;
            distanceTraveled = pawnInTurn.GetComponent<DistanceTraveled>();
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
