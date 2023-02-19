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
        public GameObject playerButtonPrefab;
        public GameObject playersImage;
        public GameObject jonesImage;

        private int maxPlayers = 4;
        private int selectedPlayers = 1;

        private void Start()
        {
            jonesImage.SetActive(false);
            playersImage.SetActive(true);
            headerText.text = "Select Number of Players";
            for (int i = 1; i <= maxPlayers; i++)
            {
                GameObject button = Instantiate(playerButtonPrefab, playersImage.transform);
                button.GetComponentInChildren<TMP_Text>().text = i.ToString();
                int playerCount = i;
                button.GetComponent<Button>().onClick.AddListener(() => SelectPlayers(playerCount));
            }
        }

        private void SelectPlayers(int playerCount)
        {
            jonesImage.SetActive(true);
            playersImage.SetActive(false);
            selectedPlayers = playerCount;
            headerText.text = "Play Against Sir Jones?";
            foreach (Transform child in jonesImage.transform)
            {
                Destroy(child.gameObject);
            }

            GameObject yesButton = Instantiate(playerButtonPrefab, jonesImage.transform);
            yesButton.GetComponentInChildren<TMP_Text>().text = "Yes";
            yesButton.GetComponent<Button>().onClick.AddListener(() => StartGame(selectedPlayers, true));

            GameObject noButton = Instantiate(playerButtonPrefab, jonesImage.transform);
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

