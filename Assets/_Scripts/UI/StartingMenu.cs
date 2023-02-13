using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


namespace BWV
{
    public class StartingMenu : MonoBehaviour
    {
        public delegate void GameStartEvent(int playerCount, bool playAgainstAI);
        public static event GameStartEvent OnGameStart;

        public TMP_Text headerText;
        public GameObject playerSelectionParent;
        public GameObject playerButtonPrefab;

        private int maxPlayers = 4;
        private int selectedPlayers = 1;

        private void Start()
        {
            headerText.text = "Select Number of Players";
            for (int i = 1; i <= maxPlayers; i++)
            {
                GameObject button = Instantiate(playerButtonPrefab, playerSelectionParent.transform);
                button.GetComponentInChildren<TMP_Text>().text = i.ToString();
                int playerCount = i;
                button.GetComponent<Button>().onClick.AddListener(() => SelectPlayers(playerCount));
            }
        }

        private void SelectPlayers(int playerCount)
        {
            selectedPlayers = playerCount;
            headerText.text = "Play Against Sir Jones?";
            foreach (Transform child in playerSelectionParent.transform)
            {
                Destroy(child.gameObject);
            }

            GameObject yesButton = Instantiate(playerButtonPrefab, playerSelectionParent.transform);
            yesButton.GetComponentInChildren<TMP_Text>().text = "Yes";
            yesButton.GetComponent<Button>().onClick.AddListener(() => StartGame(selectedPlayers, true));

            GameObject noButton = Instantiate(playerButtonPrefab, playerSelectionParent.transform);
            noButton.GetComponentInChildren<TMP_Text>().text = "No";
            noButton.GetComponent<Button>().onClick.AddListener(() => StartGame(selectedPlayers, false));
        }

        private void StartGame(int selectedPlayers, bool againstSirJones)
        {
            ClosePanel();
            OnGameStart(selectedPlayers, againstSirJones);
            GameState.InGame();
            Debug.Log("Starting game with " + selectedPlayers + " players, against Sir Jones: " + againstSirJones);
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}

