using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BWV
{
    public class StructurePanel : MonoBehaviour
    {
        public float workSubdivision = 30f;
        public TMP_Dropdown actionDropdown;
        public Button jobButton;
        public Button workButton;
        public Button closeButton;
        public Button shopButton;
        public TMP_Text text_Header;
        public TMP_Text text_Paragraph;
        private JobListSO jobList;
        private Pawn _pawn;
        public Image structureSprite;
        private StructureSO structSO;

        private void Start()
        {

            jobButton.onClick.AddListener(DropDownAction);
            workButton.onClick.AddListener(WorkAction);            
            closeButton.onClick.AddListener(Close);
            this.gameObject.SetActive(false);
        }

        public void Open(StructureSO data)
        {
            structSO = data;
            _pawn = RulesManager._pawn;
            structureSprite.sprite = structSO.structureSprite;
            jobList = structSO.structureJobs;
            ConfigButtons();
            this.gameObject.SetActive(true);
            text_Header.text = structSO.structureName;
            text_Paragraph.text = structSO.structureDescription;
            GameState.Pause();
        }

        public void ConfigButtons()
        {
            jobButton.gameObject.SetActive(jobList != null && jobList.availableJobs.Count > 0);
            workButton.gameObject.SetActive(false);

            if (_pawn.pawnJob!= null) 
            {
                foreach(var job in jobList.availableJobs)
                {
                    if (job == _pawn.pawnJob.jobSO)
                        workButton.gameObject.SetActive(true); 
                }              
            }            
        }

        void Close()
        {
            GameState.InGame();
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

        void WorkAction()
        {
            if (_pawn.SpendTime(workSubdivision))
                _pawn.pawnGoals.gold += _pawn.pawnJob.currentWage;

            else UIManager.Inst.FailToSpendTime(workSubdivision);
        }

        void DropDownAction()
        {            
            switch (actionDropdown.value)
            {
                case 0:
                    if (_pawn.SpendTime(workSubdivision))
                        _pawn.pawnGoals.gold += _pawn.pawnJob.currentWage;
                    else UIManager.Inst.FailToSpendTime(workSubdivision);
                    break;
                case 1:
                    if (_pawn.SpendTime(workSubdivision))
                        _pawn.pawnGoals.favor += 1;
                    else UIManager.Inst.FailToSpendTime(workSubdivision);
                    break;
                case 2:
                    if (_pawn.SpendTime(workSubdivision))
                        _pawn.pawnGoals.happiness += 1;
                    else UIManager.Inst.FailToSpendTime(workSubdivision);
                    break;
                default:
                    break;
            }
            UIManager.Inst.statsPanel.RefreshGoals(_pawn.pawnGoals);
        }
        
    }
}
