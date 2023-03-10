
using System.Collections;
using UnityEngine;

namespace BWV
{
    public class StructureSpawner : MonoBehaviour
    {
        public Transform spawnArea;
        public float spawnRadius = 10f;
        public LayerMask spawnAreaLayer;
        public float overlapArea = 1.2f;
        public float spawnSpeed = .2f;
        public float animatioSpeed = 2f;
        public float _minGap = .01f;

        private int overlapCount;
        private bool isLanded = false;

        public void SpawnStructure(StructureSO structPrefab)
        {
            Vector3 spawnPosition = GetRandomVector3(spawnArea.position, spawnRadius);

            // Ensure the structure is aligned with the terrain by setting its y position to the height of the terrain at the chosen position
            float terrainHeight = Terrain.activeTerrain.SampleHeight(spawnPosition);
            spawnPosition.y = terrainHeight;
            if (overlapCount > 3) overlapArea = 1;

            // Check if there is a structure already in the spawn position
            Collider[] overlapColliders = Physics.OverlapBox(spawnPosition, structPrefab.structurePrefab.GetComponent<BoxCollider>().size * overlapArea, Quaternion.identity);
            bool isOverlapping = false;
            foreach (Collider collider in overlapColliders)
            {
                if (collider.CompareTag("Structure"))
                {
                    overlapCount++;
                    isOverlapping = true;
                    break;
                }
            }

            if (!isOverlapping)
            {

                overlapCount = 0;
                // Instantiate the structure at the chosen position
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                GameObject _ = Instantiate(structPrefab.structurePrefab, spawnPosition, randomRotation);
                StructureInteraction _S = _.GetComponent<StructureInteraction>();
                _S.dataStructure = structPrefab;
                StartCoroutine(_S.LandStructure(Terrain.activeTerrain.GetPosition().y, _minGap, animatioSpeed));
            }
            else
            {
                // Try to spawn a structure again if there is an overlap
                SpawnStructure(structPrefab);
            }
        }
        public Vector3 GetRandomVector3(Vector3 center, float radius)
        {
            float angle1 = Random.Range(0, Mathf.PI * 2);
            float angle2 = Random.Range(0, Mathf.PI * 2);
            float x = radius * Mathf.Sin(angle1) * Mathf.Cos(angle2);
            float y = radius * Mathf.Sin(angle1) * Mathf.Sin(angle2);
            float z = radius * Mathf.Cos(angle1);
            return center + new Vector3(x, y, z);
        }
        
    }
}
