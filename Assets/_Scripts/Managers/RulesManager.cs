using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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
    private bool pawnActive;
    public static int humanPlayers = 0;

    private void OnEnable()
    {
        TurnPanel.OnCloseTurnPanel += TurnStart;
        StartingMenu.OnGameStart += GameStart;
        GameState.OnStateChange += GameStateHandle;
    }
    void Start()
    {
        agent.gameObject.SetActive(false);
        
    }
    private void OnDisable()
    {
        TurnPanel.OnCloseTurnPanel -= TurnStart;
        StartingMenu.OnGameStart -= GameStart;
        GameState.OnStateChange -= GameStateHandle;
    }

    void Update()
    {
        if (pawnActive && distanceTraveled.distanceTraveled > maxMovement)
        {
            TurnEnd();
        }
    }
    public void GameStart(int players, bool sirJones)
    {
        startingPosition = agent.transform.position;
        humanPlayers = players;
        TurnStart();
    }
    void TurnStart()
    {
        agent.transform.position = startingPosition;
        distanceTraveled.countingDistance = true;
        distanceTraveled.distanceTraveled = 0f;
        pawnActive = true;
    }

    void TurnEnd()
    {
        pawnActive = false;
        distanceTraveled.countingDistance = false;
        agent.isStopped = true;
        agent.ResetPath();
        GameState.Pause();
        OnTurnEnd();
        turnCount++;
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
