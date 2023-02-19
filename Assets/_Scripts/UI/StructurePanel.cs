using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BWV
{
    public class StructurePanel : MonoBehaviour
    {
        public TMP_Dropdown actionDropdown;
        public Button jobButton;
        public Button acceptButton;
        public Button closeButton;
        public TMP_Text text_Header;
        public TMP_Text text_Paragraph;
        private JobListSO jobList;
        private Pawn _pawn;
        public Image structureSprite;
        private StructureSO structSO;

        private void Start()
        {
            jobButton.onClick.AddListener(JobButton);
            acceptButton.onClick.AddListener(AcceptAction);            
            closeButton.onClick.AddListener(Close);
            this.gameObject.SetActive(false);
        }
        private void JobButton()
        {
            // Clear any existing options in the dropdown
            actionDropdown.ClearOptions();

            // Add each job in the availableJobs list to the dropdown
            foreach (var job in jobList.availableJobs)
            {
                // Add the job name as an option in the dropdown
                actionDropdown.options.Add(new TMP_Dropdown.OptionData(job.name));
            }

            // Select the first job in the dropdown
            if (actionDropdown.options.Count > 0)
            {
                actionDropdown.value = 0;
            }
            actionDropdown.onValueChanged.AddListener(OnJobSelected);
        }

        private void OnJobSelected(int jobId)
        {
            JobSO job = jobList.availableJobs[jobId];
            if(JobManager.TryAssignJob(_pawn, job))
            {
                _pawn.pawnJob = new Job(_pawn, job);
            }
        }

        void AcceptAction()
        {            
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
        public void Open(StructureSO data)
        {            
            structSO = data;
            _pawn = RulesManager._pawn;
            structureSprite.sprite = structSO.structureSprite;
            jobList = structSO.structureJobs;
            this.gameObject.SetActive(true);
            text_Header.text = structSO.structureName;
            text_Paragraph.text = structSO.structureDescription;
            GameState.Pause();
        }
        void Close()
        {
            this.gameObject.SetActive(false);
            GameState.InGame();
        }
    }
}
