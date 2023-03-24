using System.Collections;
using UnityEngine;

namespace BWV
{
    public class StructureInteraction : MonoBehaviour
    {
        public GameObject interactionMenu;
        public Transform entranceStructure;
        public bool isSelected;
        public StructureSO dataStructure;

        private void Start()
        {
            this.name = dataStructure.structureName;
        } 

        //TODO Better method to detect player enter building
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PawnMovement pm = other.GetComponent<PawnMovement>();
                if (pm.targetStrucure == dataStructure.structureType)
                {
                    Debug.Log("entered target " + this.name);
                    UIManager.Inst.structurePanel.Open(dataStructure);
                    GameState.Pause();
                    pm.targetStrucure = StructureType.none;
                }
            }
        }
        public IEnumerator LandStructure( float h, float _minGap, float animatioSpeed)
        {
            Vector3 startPosition = this.transform.position;
            Vector3 finalPosition = startPosition;
            finalPosition.y += h;

            while (Vector3.Distance(this.transform.localPosition, finalPosition) > _minGap)
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, finalPosition, Time.deltaTime * animatioSpeed);
                yield return null;
            }
            this.transform.position = finalPosition;
        }
    }
}