using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BWV
{
    public class StructureInteraction : MonoBehaviour
    {
        public GameObject interactionMenu;
        public Transform entranceStructure;
        public TMP_Dropdown actionDropdown;
        private Button acceptButton;
        private Button closeButton;
        public bool isSelected;
        private PawnStats pawnStats;

        public SO_Structure dataStructure;

        private void Start()
        {
            SetUI();
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
                    interactionMenu.SetActive(true);
                    GameState.Pause();
                    pawnStats = other.GetComponent<PawnStats>();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                pawnStats = null;
            }
        }

        void AcceptAction()
        {
            switch (actionDropdown.value)
            {
                case 0:
                    if (pawnStats.SpendTime(20f))
                        pawnStats.stats.gold += 10;
                    else UIManager.Inst.FailToSpendTime(20f);
                    break;
                case 1:
                    if (pawnStats.SpendTime(20f))
                        pawnStats.stats.favor += 1;
                    else UIManager.Inst.FailToSpendTime(20f);
                    break;
                case 2:
                    if (pawnStats.SpendTime(20f))
                        pawnStats.stats.happiness += 1;
                    else UIManager.Inst.FailToSpendTime(20f);
                    break;
                default:
                    break;
            }
            UIManager.Inst.statsPanel.RefreshStats(pawnStats.stats);
        }
        private void SetUI()
        {
            interactionMenu = UIManager.Inst.structurePanel;
            interactionMenu.SetActive(false);
            acceptButton = interactionMenu.transform.Find("Button_Do").GetComponent<Button>();
            acceptButton.onClick.AddListener(AcceptAction);
            closeButton = interactionMenu.transform.Find("Button_Exit").GetComponent<Button>();
            closeButton.onClick.AddListener(CloseMenu);
        }
        void CloseMenu()
        {
            interactionMenu.SetActive(false);
            GameState.InGame();
        }
    }
}