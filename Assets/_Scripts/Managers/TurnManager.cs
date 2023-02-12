
using UnityEngine;
using UnityEngine.Events;

public class TurnManager
{
    public static event UnityAction OnTurnEnd;
    public static event UnityAction OnTurnStart;

    private static int humanPlayers;
    private static int turnCount = 0;
    private static int playerInTurn = 0;
    private static bool pawnActive = false;

    public static void Initialize(int players, bool sirJones)
    {
        humanPlayers = players;
        TurnStart();
    }

    private static void TurnStart()
    {
        pawnActive = true;
        OnTurnStart?.Invoke();
    }

    public static void EndTurn()
    {
        pawnActive = false;
        OnTurnEnd?.Invoke();
        turnCount++;
    }
}