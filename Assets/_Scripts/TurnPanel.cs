using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class TurnPanel : MonoBehaviour
{
    public Button continueButton;
    public TMP_Text endTurnMessage;
    public static event UnityAction OnCloseTurnPanel;

    private void Start()
    {
        continueButton.onClick.AddListener(ClosePanel);
        RulesManager.OnTurnEnd += ShowPanel;
        RulesManager.OnTurnStart += ClosePanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        RulesManager.OnTurnEnd -= ShowPanel;
        RulesManager.OnTurnStart -= ClosePanel;
    }

    private void ShowPanel()
    {
        endTurnMessage.text = "Your turn has ended. You gone far enougth.";
        gameObject.SetActive(true);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
        OnCloseTurnPanel?.Invoke();
    }
}
