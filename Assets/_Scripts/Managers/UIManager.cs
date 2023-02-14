using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BWV
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Inst;

        public GameObject playerPanel;
        public GameObject turnPanel;
        public GameObject gameOverPanel;
        public GameObject startPanel;
        public GameObject pausePanel;

        public StructurePanel structurePanel;
        public StatsPanel statsPanel;

        public Button closePanelButton;
        public TMP_Text stateText;
        public TMP_Text distanceText;
        public Slider timeSlider;

        private Button pauseButton;
        private void Awake()
        {
            if (Inst == null)
            {
                Inst = this;
            }
            else if (Inst != this)
            {
                Debug.Log("destroyed " + this.name);
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            GameState.OnStateChange += GameStateHandle;
        }

        private void OnDisable()
        {
            GameState.OnStateChange -= GameStateHandle;
        }
        private void Start()
        {
            SetPauseUI();
        }

        private void PauseMenu()
        {
            pausePanel.SetActive(true);
            GameState.Pause();
        }

        private void SetPauseUI()
        {
            pausePanel.SetActive(false);
            pauseButton = playerPanel.transform.Find("Button_Pause").GetComponent<Button>();
            pauseButton.onClick.AddListener(PauseMenu);
        }

        public void RefreshTimeSlider(float maxTime, float currentTime)
        {
            timeSlider.maxValue = maxTime;
            timeSlider.value= currentTime;
        }
        public void FailToSpendTime(float timeDenied)
        {
            Debug.LogWarning("FAILED TO SPEND TIME " + timeDenied);
        }

        void GameStateHandle(GameState.State state)
        {
            stateText.text = state.ToString();
            switch (state)
            {
                case GameState.State.InGame:
                    statsPanel.Open();
                    break;
                case GameState.State.MenuStart:
                    statsPanel.Close();
                    break;
                case GameState.State.Intro:
                    statsPanel.Close();
                    break;
                default:
                    break;
            }
        }
    }

   
}