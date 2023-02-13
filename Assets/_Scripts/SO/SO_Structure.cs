using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "Data_Structure", menuName = "ScriptableObjects/SO_Structure", order = 1)]
    public class SO_Structure : ScriptableObject
    {
        [Header("Structure Information")]
        public string structureName;
        public string structureDescription;
        public Vector3 structurePosition;
        public Color[] structurePallete;
        public Material structureMaterial;
        public StructureType structureType;
        public GameObject structurePrefab;
        public GameObject structurePanel;
        public bool structureOpen;

    }
}