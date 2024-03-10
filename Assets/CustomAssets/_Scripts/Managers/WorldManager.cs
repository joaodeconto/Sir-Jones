using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace BWV
{
    public class WorldManager : MonoBehaviour
    {

        #region Events
        public static event UnityAction OnStructuresSpawned;
        #endregion

        public static WorldManager Inst { get; private set; }

        public StructureSO[] structurePrefabs;
        public StructureSpawner structureSpawner;
        public NavMeshRebuilder navMeshRebuild;
        public int maxStructures = 15;
        private bool isSpawning;

        private void Start()
        {
            Inst = this;
        }
        public void GenerateWorld()
        {
            StartCoroutine(IsGeneratingWorld());
        }

        public IEnumerator IsGeneratingWorld()
        {
            // Spawn random structures
            yield return SpawnStructures();

            while(isSpawning) 
                yield return null;
            // Rebuild the NavMesh
            yield return RebuildNavMesh();

            OnStructuresSpawned();
            // Set the game state to InGame
            GameState.InGame();
        }

        private IEnumerator SpawnStructures()
        {
            isSpawning = true;
            for (int i = 0; i < structurePrefabs.Length; i++)
            {
                structureSpawner.SpawnStructure(structurePrefabs[i]);
                // Wait for a short time before spawning the next structure
                yield return new WaitForSeconds(structureSpawner.spawnSpeed);
            }
            isSpawning= false;
        }

        private IEnumerator RebuildNavMesh()
        {
            StartCoroutine(navMeshRebuild.RebuildNavMeshCoroutine());
            return null;
        }
    }
}
