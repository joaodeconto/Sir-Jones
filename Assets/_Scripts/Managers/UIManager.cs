using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Inst;

    public GameObject playerPanel;
    public GameObject structurePanel;
    public GameObject turnPanel;
    public GameObject gameOverPanel;
    public GameObject startPanel;
    public GameObject pausePanel;
    public Button closePanelButton;
    public TMP_Text stateText;

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
        GameState.OnStateChange += ShowGameState;
    }

    private void OnDisable()
    {
        GameState.OnStateChange -= ShowGameState;
    }
    private void Start()
    {
        SetPauseUI();
    }

    private void ShowGameState(GameState.State state)
    {
        stateText.text= state.ToString();
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
}