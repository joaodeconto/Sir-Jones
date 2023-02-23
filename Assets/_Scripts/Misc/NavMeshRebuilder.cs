using System.Collections;
using UnityEngine;
using Unity.AI.Navigation;


namespace BWV
{
    public class NavMeshRebuilder : MonoBehaviour
    {
        public NavMeshSurface navMeshSurface;

        public IEnumerator RebuildNavMeshCoroutine()
        {
            Debug.Log("aaqqui");
                navMeshSurface.BuildNavMesh();
                yield return null;
        }
    }
}
