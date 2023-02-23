
using UnityEngine;

namespace BWV
{
    public class StructureSpawner : MonoBehaviour
    {
        public Transform spawnArea;
        public float spawnRadius = 10f;
        public LayerMask spawnAreaLayer;


        public void SpawnStructure(StructureSO structPrefab)
        {
            Vector3 spawnPosition = GetRandomVector3(spawnArea.position, spawnRadius);

            // Ensure the structure is aligned with the terrain by setting its y position to the height of the terrain at the chosen position
            float terrainHeight = Terrain.activeTerrain.SampleHeight(spawnPosition);
            spawnPosition.y = Terrain.activeTerrain.transform.position.y + terrainHeight;

            // Check if there is a structure already in the spawn position
            Collider[] overlapColliders = Physics.OverlapBox(spawnPosition, structPrefab.structurePrefab.GetComponent<BoxCollider>().size / 2f, Quaternion.identity);
            bool isOverlapping = false;
            foreach (Collider collider in overlapColliders)
            {
                if (collider.CompareTag("Structure"))
                {
                    isOverlapping = true;
                    break;
                }
            }

            if (!isOverlapping)
            {
                // Instantiate the structure at the chosen position
                GameObject _ = Instantiate(structPrefab.structurePrefab, spawnPosition, Quaternion.identity);
                _.GetComponent<StructureInteraction>().dataStructure = structPrefab;
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
