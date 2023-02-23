using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

namespace BWV
{
    public class WorldManager : MonoBehaviour
    {
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
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
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
