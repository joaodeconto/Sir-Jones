using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace BWV
{
    public class TurnPanel : MonoBehaviour
    {
        public Button continueButton;
        public TMP_Text endTurnMessage;
        public static event UnityAction OnCloseTurnPanel;

        private void Start()
        {
            continueButton.onClick.AddListener(ClosePanel);
            gameObject.SetActive(false);
            RulesManager.OnTurnEnd += ShowPanel;
        }
        private void OnDestroy()
        {
            RulesManager.OnTurnEnd -= ShowPanel;
        }
        private void ShowPanel()
        {
            endTurnMessage.text = "Your turn has ended.";
            gameObject.SetActive(true);
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
            OnCloseTurnPanel();
        }
    }
}
