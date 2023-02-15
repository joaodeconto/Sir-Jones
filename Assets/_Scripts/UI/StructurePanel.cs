using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BWV
{
    public class StructurePanel : MonoBehaviour
    {
        public TMP_Dropdown actionDropdown;
        private Button acceptButton;
        private Button closeButton;
        private TMP_Text text_Header;
        private TMP_Text text_Paragraph;

        private void Start()
        {            
            text_Header = transform.Find("Text_Header").GetComponent<TMP_Text>();
            text_Paragraph = transform.Find("Text_Paragraph").GetComponent<TMP_Text>();
            acceptButton = transform.Find("Button_Do").GetComponent<Button>();
            acceptButton.onClick.AddListener(AcceptAction);
            closeButton = transform.Find("Button_Exit").GetComponent<Button>();
            closeButton.onClick.AddListener(Close);
            this.gameObject.SetActive(false);
        }
        void AcceptAction()
        {
            Pawn _pawn = RulesManager._pawn;
            switch (actionDropdown.value)
            {
                case 0:
                    if (_pawn.SpendTime(20f))
                        _pawn.pawnGoals.gold += 10;
                    else UIManager.Inst.FailToSpendTime(20f);
                    break;
                case 1:
                    if (_pawn.SpendTime(20f))
                        _pawn.pawnGoals.favor += 1;
                    else UIManager.Inst.FailToSpendTime(20f);
                    break;
                case 2:
                    if (_pawn.SpendTime(20f))
                        _pawn.pawnGoals.happiness += 1;
                    else UIManager.Inst.FailToSpendTime(20f);
                    break;
                default:
                    break;
            }
            UIManager.Inst.statsPanel.RefreshGoals(_pawn.pawnGoals);
        }
        public void Open(SO_Structure data)
        {
            this.gameObject.SetActive(true);
            text_Header.text = data.structureName;
            text_Paragraph.text = data.structureDescription;
            GameState.Pause();
        }
        void Close()
        {
            this.gameObject.SetActive(false);
            GameState.InGame();
        }
    }
}
