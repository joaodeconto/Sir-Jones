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

    void Start()
    {
        TurnPanel.OnCloseTurnPanel += TurnStart;
        StartingMenu.OnGameStart += GameStart;
    }
    private void OnDestroy()
    {
        TurnPanel.OnCloseTurnPanel -= TurnStart;
    }

    void Update()
    {
        if (distanceTraveled.distanceTraveled > maxMovement)
        {
            EndTurn();
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
        agent.ResetPath();
        agent.transform.position = startingPosition;
        pawnActive = true;
        distanceTraveled.countingDistance = true;
        distanceTraveled.distanceTraveled = 0f;
        OnTurnStart?.Invoke();
    }

    void EndTurn()
    {
        pawnActive = false;
        distanceTraveled.countingDistance = false;
        agent.isStopped = true;
        agent.ResetPath();
        OnTurnEnd?.Invoke();
        turnCount++;
    }
}
