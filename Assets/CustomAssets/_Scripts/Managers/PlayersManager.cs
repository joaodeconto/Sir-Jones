using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace BWV
{
    public class PlayersManager : MonoBehaviour
    {
        #region Fields
        public PawnSO[] playersPreset;
        public static List<GameObject> playersIngame = new();
        public static event UnityAction OnPlayersSetup;

        public Vector3 startingRef;

        private int totalPlayers;
        private bool sirJones;
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            StartingMenu.OnGameStart += SetupPlayers;
            WorldManager.OnStructuresSpawned += SpawnPlayers;
        }

        private void OnDisable()
        {
            StartingMenu.OnGameStart -= SetupPlayers;
            WorldManager.OnStructuresSpawned -= SpawnPlayers;
        }
        #endregion

        void SetupPlayers(int humanPlayers, bool _sirJones)
        {
            totalPlayers= humanPlayers;
            sirJones = _sirJones;
        }

        void SpawnPlayers()
        { 
            //GAMBI Sir jones is player 0, so if !sirjones start on 1
            int b = sirJones ? 0 : 1;
            foreach (StructureStats ss in StructureSpawner.StructuresList)
            {
                if (ss.structureData.structureType == StructureType.Slum)
                {
                    NavMeshHit myNavHit;
                    if (NavMesh.SamplePosition(ss.transform.position, out myNavHit, 20, -1))
                    {
                        startingRef = myNavHit.position;

                    }
                }
            }

            for (int i = b; i <= totalPlayers; i++)
            {
                //playersPreset[i].playerNumber = i + 1;
                //playersPreset[i].turnOrder = i;

                GameObject pawn = Instantiate(playersPreset[i].pawnPrefab, startingRef, Quaternion.identity);
                Pawn _ = pawn.GetComponent<Pawn>();
                _.pawnData = playersPreset[i];
                playersIngame.Add(pawn);
            }
            OnPlayersSetup();
        }
    }
}

