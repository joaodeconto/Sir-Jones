using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public SO_Pawn[] players;

    void Start()
    {
        int playerCount = RulesManager.humanPlayers;
        players = new SO_Pawn[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            players[i] = new SO_Pawn();
            players[i].playerNumber = i + 1;
            players[i].turnOrder = i;

            // Set player name, color, starting position, and pawn prefab as desired
        }
    }
}


