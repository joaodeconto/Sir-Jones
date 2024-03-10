using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

namespace BWV
{
    public class StructureStats : MonoBehaviour
    {
        public StructureSO structureData;
        public Transform structureDoor;
        public MeshRenderer[] meshRenderer;
        public TMP_Text structureSign;
        public Light[] structureLights;

        private void OnEnable()
        {
            DayNightManager.OnDayNightShift += ActivateLight;
        }
        private void OnDisable()
        {
            DayNightManager.OnDayNightShift -= ActivateLight;
        }

        private void Start()
        {
            structureData = gameObject.GetComponent<StructureInteraction>().dataStructure; 
            //structureSign.text = structureData.structureName;
            foreach(MeshRenderer renderer in meshRenderer)
            {
                renderer.material = structureData.structureMaterial;
            }
            ActivateStruct();
            
        }

        private void ActivateLight(bool active) 
        {
            foreach(Light light in structureLights)
            {
                light.gameObject.SetActive(active);
            }         
        }

        private void ActivateStruct()
        {
            ActivateLight(false);
            structureSign.text = structureData.structureName;
            structureSign.transform.localRotation = Quaternion.Inverse(structureSign.transform.root.rotation);
        }
    }

}