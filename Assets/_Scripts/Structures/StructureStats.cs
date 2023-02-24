using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

namespace BWV
{
    public class StructureStats : MonoBehaviour
    {
        private StructureSO structureData;
        public MeshRenderer[] meshRenderer;
        public TMP_Text structSign;

        private void Start()
        {
            structureData = gameObject.GetComponent<StructureInteraction>().dataStructure; 
            structSign.text = structureData.structureName;
            foreach(MeshRenderer renderer in meshRenderer)
            {
                renderer.material = structureData.structureMaterial;
            }
            ActivateStruct();
        }

        private void ActivateStruct()
        {
            structSign.text = structureData.structureName;
            structSign.transform.localRotation = Quaternion.Inverse(structSign.transform.root.rotation);
        }
    }

}