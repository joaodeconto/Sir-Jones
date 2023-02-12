using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StructureInteraction : MonoBehaviour
{
    public GameObject interactionMenu;
    public TMP_Dropdown actionDropdown;
    private Button acceptButton;
    private Button closeButton;
    public bool isSelected;

    public SO_Structure dataStructure;

    private void Start()
    {
        SetUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PawnMovement>().targetStrucure = this)
            {
                Debug.Log("entered target " + this.name);
                interactionMenu.SetActive(true);
                GameState.Pause();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //interactionMenu.SetActive(false);
            //StateManager.instance.SetState(StateManager.State.Move);
        }
    }

    void AcceptAction()
    {
        switch (actionDropdown.value)
        {
            case 0:
                // Perform action 1
                break;
            case 1:
                // Perform action 2
                break;
            case 2:
                // Perform action 3
                break;
            default:
                break;
        }
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