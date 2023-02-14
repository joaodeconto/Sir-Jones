using UnityEngine;

namespace BWV
{
    public class StructureInteraction : MonoBehaviour
    {
        public GameObject interactionMenu;
        public Transform entranceStructure;
        public bool isSelected;
        public SO_Structure dataStructure;

        private void Start()
        {
            this.name = dataStructure.structureName;
        }

        //TODO Better method to detect player enter building
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //TODO Pawn instance?
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

        private void OnTriggerExit(Collider other)
        {

        }
    }
}